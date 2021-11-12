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

namespace Amadeus.Server.Services
{
	public class UserService
	{
		/// <summary>
		/// The User ORM.
		/// </summary>
		private readonly UserDB _context;


		/// <summary>
		/// Get all the users in db.
		/// </summary>
		/// <returns>All the users in the db.</returns>
		public Task<List<User>> GetAll()
		{
			return _context.Users
				.AsNoTracking()
				.Select(o => new User
				{
					Username = o.Username,
					Email = o.Email
				})
				.ToListAsync();
		}

		/// <summary>
		/// Get a user by it's id.
		/// </summary>
		/// <param name="id">The id of the user to get.</param>
		/// <returns>The user corresponding at the id.</returns>
		public Task<User> GetUserById(ulong id)
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
			_context.Add(user);
			await _context.SaveChangesAsync();
			return user;
		}

		/// <summary>
		/// Delete a user by id.
		/// </summary>
		/// <param name="id">id of the user to delete.</param>
		/// <returns>True if the user was deleted false otherwise</returns>
		public async Task<bool> Delete(ulong id)
		{
			bool isDeleted = false;
			User user = await GetUserById(id);

			if (user != null)
			{
				_context.Remove(user);
				await _context.SaveChangesAsync();
				isDeleted = true;
			}

			return isDeleted;
		}
	}
}
