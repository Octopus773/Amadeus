using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Amadeus.Server.Data;
using Amadeus.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Amadeus.Server.Services
{
	/// <summary>
	/// The orm class for the user object.
	/// </summary>
	public class UserService
	{
		/// <summary>
		/// The User ORM.
		/// </summary>
		private readonly ServerDB _context;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="context">The DB to use when storing and retrieving data.</param>
		public UserService(ServerDB context)
		{
			_context = context;
		}


		/// <summary>
		/// Get all the users in db.
		/// </summary>
		/// <returns>All the users in the db.</returns>
		public Task<List<User>> GetAll()
		{
			return _context.Users
				.AsNoTracking()
				.ToListAsync();
		}

		/// <summary>
		/// Get a user by it's id.
		/// </summary>
		/// <param name="id">The id of the user to get.</param>
		/// <returns>The user corresponding at the id.</returns>
		public Task<User> GetUserById(int id)
		{
			return _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
		}

		/// <summary>
		/// Create saves the user in the db.
		/// </summary>
		/// <param name="user">The user to save in the db.</param>
		/// <returns>The user saved in db.</returns>
		public async Task<User> Create(User user)
		{
			Debug.Assert(user != null, nameof(user) + " != null");
			user.CreatedAt = DateTime.UtcNow;

			// generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
			byte[] salt = new byte[128 / 8];
			using (var rngCsp = new RNGCryptoServiceProvider())
			{
				rngCsp.GetNonZeroBytes(salt);
			}

			// derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
			user.Password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: user.Password,
				salt: salt,
				prf: KeyDerivationPrf.HMACSHA256,
				iterationCount: 100000,
				numBytesRequested: 256 / 8));

			_context.Add(user);
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (Microsoft.EntityFrameworkCore.DbUpdateException exception)
			{
				throw new ArgumentException("User with fields like this already exists");
			}

			return user;
		}

		/// <summary>
		/// Delete a user by id.
		/// </summary>
		/// <param name="id">id of the user to delete.</param>
		/// <returns>True if the user was deleted false otherwise</returns>
		public async Task<User> Delete(int id)
		{
			User user = await GetUserById(id);

			if (user != null)
			{
				_context.Remove(user);
				await _context.SaveChangesAsync();
			}

			return user;
		}
	}
}
