using System;
using System.Collections.Generic;
using Amadeus.Server.Models;
using Amadeus.Server.Services;
using Amadeus.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Amadeus.Server.Controllers
{
	/// <summary>
	/// The controller for user related interactions.
	/// </summary>
	[ApiController]
	[Route("user")]
	public class UserController : ControllerBase
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
		[HttpGet]
		public ActionResult<List<User>> GetUsers()
		{
			return new List<User>();
			// return _userService.GetAll();
		}

		/// <summary>
		/// Get a specific user.
		/// </summary>
		/// <param name="uid">Uid of the specific user.</param>
		/// <returns>All infos of the specific user.</returns>
		[HttpGet("/{uid:long}")]
		public IActionResult GetUser(ulong uid)
		{
			// return _userService.GetUserById(uid);
			return NotFound();
		}

		/// <summary>
		/// Create a user.
		/// </summary>
		/// <param name="user">The user to create.</param>
		/// <returns>The infos of the newly created user.</returns>
		[HttpPost]
		public IActionResult CreateUser(User user)
		{
			_userService.Create(user).Wait();
			return CreatedAtAction(nameof(CreateUser), new { uid = 42 }, user);
		}

		/// <summary>
		/// Modify a specific user.
		/// </summary>
		/// <param name="uid">The id of the user to modify.</param>
		/// <param name="user">The new infos to update.</param>
		/// <returns>The specific user with updated infos.</returns>
		[HttpPut("/{uid:long}")]
		public IActionResult ModifyUser(ulong uid, User user)
		{
			return NotFound();
		}

		/// <summary>
		/// Delete a specific user.
		/// </summary>
		/// <param name="uid">The id of the specific user.</param>
		/// <returns>The infos of the deleted user.</returns>
		[HttpDelete("/{uid:long}")]
		public IActionResult DeleteUser(ulong uid)
		{
			_userService.Delete(uid).Wait();
			return NoContent();
		}

	}
}
