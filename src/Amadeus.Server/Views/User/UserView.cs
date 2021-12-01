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
				return await _userRepository.GetById(uid);
			}
			catch (ElementNotFound e)
			{
				return NotFound(e.Message);
			}
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
		public async Task<ActionResult<User>> ModifyUser(int uid, [NotNull] UserModificationDto userDto)
		{
			User user;
			try
			{
				user = await _userRepository.GetById(uid);
			}
			catch (ElementNotFound e)
			{
				return NotFound(e.Message);
			}
			if (userDto == null)
			{
				return BadRequest("Missing update infos");
			}

			user.Email = userDto.Email ?? user.Email;
			user.Username = userDto.Username ?? user.Username;
			user.Password = userDto.Password ?? user.Password;

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
