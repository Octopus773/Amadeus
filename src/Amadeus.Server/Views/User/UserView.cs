using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amadeus.Server.Controllers;
using Amadeus.Server.Exceptions;
using Amadeus.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Amadeus.Server.Views
{
	/// <summary>
	/// User view that receives http requests.
	/// </summary>
	[ApiController]
	[Route("/user")]
	public class UserView : ControllerBase
	{
		private readonly IRepository<User> _userRepository;

		/// <summary>
		/// Create a new <see cref="UserView"/>.
		/// </summary>
		/// <param name="userRepository">Controller for the business logic.</param>
		public UserView(IRepository<User> userRepository)
		{
			_userRepository = userRepository;
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
		public async Task<ActionResult<IList<User>>> GetAll()
		{
			return Ok(await _userRepository.GetAll());
		}

		/// <summary>
		/// Get a specific user.
		/// </summary>
		/// <param name="uid">Uid of the specific user.</param>
		/// <returns>All infos of the specific user.</returns>
		[HttpGet("{uid:int}")]
		public async Task<ActionResult<User>> GetUser(int uid)
		{
			return await _userRepository.GetUserById(uid);
		}

		/// <summary>
		/// Create a user.
		/// </summary>
		/// <param name="user">The user to create.</param>
		/// <returns>The infos of the newly created user.</returns>
		[HttpPost]
		public async Task<IActionResult> CreateUser(User user)
		{
			try
			{
				await _userRepository.Create(user);
			}
			catch (DuplicateField exception)
			{
				return BadRequest(exception.Message);
			}
			return CreatedAtAction(nameof(CreateUser), new { uid = 42 }, user);
		}

		/// <summary>
		/// Modify a specific user.
		/// </summary>
		/// <param name="uid">The id of the user to modify.</param>
		/// <param name="user">The new infos to update.</param>
		/// <returns>The specific user with updated infos.</returns>
		[HttpPut("{uid:long}")]
		public IActionResult ModifyUser(int uid, User user)
		{
			return NotFound();
		}

		/// <summary>
		/// Delete a specific user.
		/// </summary>
		/// <param name="uid">The id of the specific user.</param>
		/// <returns>The infos of the deleted user.</returns>
		[HttpDelete("{uid:long}")]
		public async Task<ActionResult<User>> DeleteUser(int uid)
		{
			User user = await _userRepository.Delete(uid);

			if (user == null)
			{
				return NotFound();
			}

			return user;
		}
	}
}
