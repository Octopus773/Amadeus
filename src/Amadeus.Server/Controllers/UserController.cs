using System;
using System.Collections.Generic;
using Amadeus.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace Amadeus.Server.Controllers
{
	[ApiController]
	[Route("user")]
	public class UserController : ControllerBase
	{
		[HttpGet]
		public ActionResult<List<User>> getUsers()
		{
			return new List<User>();
		}

		[HttpGet("/{uid:int}")]
		public IActionResult getUser(UInt64 uid)
		{
			return NotFound();
		}

		[HttpPost]
		public IActionResult createUser(User user)
		{
			return CreatedAtAction(nameof(createUser), new { uid = 42 }, user);
		}

		[HttpPut("/{uid:int}")]
		public IActionResult modifyUser(UInt64 uid, User user)
		{
			return NotFound();
		}

		[HttpDelete("/{uid:int}")]
		public IActionResult deleteUser(UInt64 uid)
		{
			return NoContent();
		}

	}
}
