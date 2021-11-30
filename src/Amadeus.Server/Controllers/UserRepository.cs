using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Amadeus.Server.Data;
using Amadeus.Server.Exceptions;
using Amadeus.Server.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;

namespace Amadeus.Server.Controllers
{
	/// <summary>
	/// The orm class for the user object.
	/// </summary>
	public class UserRepository : IRepository<User>
	{

		/// <summary>
		/// The User ORM.
		/// </summary>
		private readonly ServerDB _context;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="context">The DB to use when storing and retrieving data.</param>
		public UserRepository(ServerDB context)
		{
			_context = context;
		}


		/// <summary>
		/// Get all the users in db.
		/// </summary>
		/// <returns>All the users in the db.</returns>
		public async Task<IList<User>> GetAll()
		{
			return await _context.Users
				.AsNoTracking()
				.ToListAsync();
		}

		/// <summary>
		/// Get a user by it's id.
		/// </summary>
		/// <param name="id">The id of the user to get.</param>
		/// <returns>The user corresponding at the id.</returns>
		public async Task<User> GetById(int id)
		{
			User u = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
			if (u == null)
			{
				throw new ElementNotFound($"The user with the id: {id} wasn't found");
			}
			return u;
		}

		/// <summary>
		/// Create saves the user in the db.
		/// </summary>
		/// <param name="user">The user to save in the db.</param>
		/// <returns>The user saved in db.</returns>
		public async Task<User> Create([NotNull] User user)
		{
			Debug.Assert(user != null, nameof(user) + " != null");

			user.CreatedAt = DateTime.UtcNow;
			user.Verified = false;
			_context.Add(user);
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				throw new DuplicateField("User with fields like this already exists");
			}

			return user;
		}

		/// <inheritdoc/>
		public async Task<User> Modify(int uid, [NotNull] User user)
		{
			User u = await GetById(uid);

			user.Id = uid;
			_context.Entry(user).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return user;
		}

		/// <summary>
		/// Delete a user by id.
		/// </summary>
		/// <param name="id">id of the user to delete.</param>
		/// <returns>True if the user was deleted false otherwise</returns>
		public async Task<User> Delete(int id)
		{
			User user = await GetById(id);

			_context.Remove(user);
			await _context.SaveChangesAsync();

			return user;
		}

		public Task<IList<User>> GetWhere(Expression<Func<User, bool>> pred)
		{
			throw new NotImplementedException();
		}
	}
}
