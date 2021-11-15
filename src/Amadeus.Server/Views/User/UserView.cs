using System.Collections.Generic;
using System.Threading.Tasks;
using Amadeus.Server.Controllers;
using Amadeus.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Amadeus.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Amadeus.Server.Views
{
	[ApiController]
	[Route("/user")]
	public class UserView : ControllerBase
	{
		private readonly UserController _userController;

		/// <summary>
		/// Create a new <see cref="UserView"/>.
		/// </summary>
		/// <param name="userController">Controller for the business logic.</param>
		public UserView(UserController userController)
		{
			_userController = userController;
		}

		/// <summary>
		/// Get all users.
		/// </summary>
		/// <remarks>
		/// It gets all the users.
		/// </remarks>
		/// <returns>All the users.</returns>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<List<Models.User>>> GetAll()
		{
			return await _userController.GetUsers();
		}

		/// <summary>
		/// Get a specific user.
		/// </summary>
		/// <param name="uid">Uid of the specific user.</param>
		/// <returns>All infos of the specific user.</returns>
		[HttpGet("{uid:long}")]
		public async Task<ActionResult<User>> GetUser(ulong uid)
		{
			return await _userController.GetUser(uid);
		}

		/// <summary>
		/// Create a user.
		/// </summary>
		/// <param name="user">The user to create.</param>
		/// <returns>The infos of the newly created user.</returns>
		[HttpPost]
		public async Task<IActionResult> CreateUser(User user)
		{
			await _userController.CreateUser(user);
			return CreatedAtAction(nameof(CreateUser), new { uid = 42 }, user);
		}

		/// <summary>
		/// Modify a specific user.
		/// </summary>
		/// <param name="uid">The id of the user to modify.</param>
		/// <param name="user">The new infos to update.</param>
		/// <returns>The specific user with updated infos.</returns>
		[HttpPut("{uid:long}")]
		public IActionResult ModifyUser(ulong uid, User user)
		{
			return NotFound();
		}

		/// <summary>
		/// Delete a specific user.
		/// </summary>
		/// <param name="uid">The id of the specific user.</param>
		/// <returns>The infos of the deleted user.</returns>
		[HttpDelete("{uid:long}")]
		public async Task<ActionResult<User>> DeleteUser(ulong uid)
		{
			User user = await _userController.DeleteUser(uid);

			if (user == null)
			{
				return NotFound();
			}

			return user;
		}
	}
}
