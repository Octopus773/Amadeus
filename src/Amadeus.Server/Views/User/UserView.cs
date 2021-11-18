using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<User>> GetUser(int uid)
		{
			try
			{
				return await _userRepository.GetUserById(uid);
			}
			catch (ElementNotFound e)
			{
				return NotFound(e.Message);
			}
		}

		/// <summary>
		/// Create a user.
		/// </summary>
		/// <param name="user">The user to create.</param>
		/// <returns>The infos of the newly created user.</returns>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> CreateUser([NotNull] UserCreationDTO userDto)
		{
			User user = new User();

			// will be in a controller.
			user.Email = userDto.Email;
			user.Password = userDto.Password;
			user.DisplayName = userDto.DisplayName.Trim();
			user.Username = user.DisplayName.Replace(" ", string.Empty).ToLower();
			try
			{
				await _userRepository.Create(user);
			}
			catch (DuplicateField exception)
			{
				return BadRequest(exception.Message);
			}
			return Created(nameof(CreateUser), user);
		}

		/// <summary>
		/// Modify a specific user.
		/// </summary>
		/// <param name="uid">The id of the user to modify.</param>
		/// <param name="user">The new infos to update.</param>
		/// <returns>The specific user with updated infos.</returns>
		[HttpPut("{uid:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<User>> ModifyUser(int uid, User user)
		{
			try
			{
				return await _userRepository.Modify(uid, user);
			}
			catch (ElementNotFound e)
			{
				return NotFound(e.Message);
			}
		}

		/// <summary>
		/// Delete a specific user.
		/// </summary>
		/// <param name="uid">The id of the specific user.</param>
		/// <returns>The infos of the deleted user.</returns>
		[HttpDelete("{uid:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<User>> DeleteUser(int uid)
		{
			try
			{
				return await _userRepository.Delete(uid);
			}
			catch (ElementNotFound e)
			{
				return NotFound(e.Message);
			}
		}
	}
}
