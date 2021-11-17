using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amadeus.Server.Models;
using Amadeus.Server.Services;
using Amadeus.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Amadeus.Server.Controllers
{
	/// <summary>
	/// The controller for user related interactions.
	/// </summary>
	public class UserController
	{
		private readonly UserService _userService;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="userService">The user orm to use.</param>
		public UserController(UserService userService)
		{
			_userService = userService;
		}

		/// <summary>
		/// Get all the registered users.
		/// </summary>
		/// <returns>The list of all registered users.</returns>
		public async Task<List<User>> GetUsers()
		{
			return await _userService.GetAll();
		}

		/// <summary>
		/// Get a specific user.
		/// </summary>
		/// <param name="uid">Uid of the specific user.</param>
		/// <returns>All infos of the specific user.</returns>
		public async Task<User> GetUser(int uid)
		{
			return await _userService.GetUserById(uid);
		}

		/// <summary>
		/// Create a user.
		/// </summary>
		/// <param name="user">The user to create.</param>
		/// <returns>The infos of the newly created user.</returns>
		public async Task<User> CreateUser(User user)
		{
			return await _userService.Create(user);
		}

		/// <summary>
		/// Modify a specific user.
		/// </summary>
		/// <param name="uid">The id of the user to modify.</param>
		/// <param name="user">The new infos to update.</param>
		/// <returns>The specific user with updated infos.</returns>
		public Task<User> ModifyUser(int uid, User user)
		{
			return null;
		}

		/// <summary>
		/// Delete a specific user.
		/// </summary>
		/// <param name="uid">The id of the specific user.</param>
		/// <returns>The infos of the deleted user.</returns>
		public async Task<User> DeleteUser(int uid)
		{
			return await _userService.Delete(uid);
		}

	}
}
