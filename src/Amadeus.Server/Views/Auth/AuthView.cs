using System;
using System.Linq;
using System.Threading.Tasks;
using Amadeus.Server.Controllers;
using Amadeus.Server.Exceptions;
using Amadeus.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Amadeus.Server.Views.Auth
{
	/// <summary>
	/// Sign in, Sign up or refresh tokens.
	/// </summary>
	[ApiController]
	[Route("auth")]
	public class AuthView : ControllerBase
	{
		/// <summary>
		/// The repository used to check if the user exists.
		/// </summary>
		private readonly IRepository<User> _users;

		/// <summary>
		/// The token generator.
		/// </summary>
		private readonly TokenController _token;

		/// <summary>
		/// Create a new <see cref="AuthView"/>.
		/// </summary>
		/// <param name="users">The repository used to check if the user exists.</param>
		/// <param name="token">The token generator.</param>
		public AuthView(IRepository<User> users, TokenController token)
		{
			_users = users;
			_token = token;
		}

		/// <summary>
		/// Login.
		/// </summary>
		/// <remarks>
		/// Login as a user and retrieve an access and a refresh token.
		/// </remarks>
		/// <param name="request">The body of the request.</param>
		/// <returns>A new access and a refresh token.</returns>
		/// <response code="400">The user and password does not match.</response>
		[HttpPost("login")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<JwtResponse>> Login([FromBody] LoginRequest request)
		{
			User user = (await _users.GetAll()).FirstOrDefault(x => x.Username == request.Username);
			if (user != null && BCryptNet.Verify(request.Password, user.Password))
			{
				return new JwtResponse
				{
					AccessToken = _token.CreateAccessToken(user, out DateTime expireDate),
					RefreshToken = await _token.CreateRefreshToken(user),
					ExpireTime = expireDate
				};
			}
			return BadRequest(new { Message = "The user and password does not match." });
		}

		/// <summary>
		/// Register.
		/// </summary>
		/// <remarks>
		/// Register a new user and get a new access/refresh token for this new user.
		/// </remarks>
		/// <param name="request">The body of the request.</param>
		/// <returns>A new access and a refresh token.</returns>
		/// <response code="400">The request is invalid.</response>
		/// <response code="409">A user already exists with this username or email address.</response>
		[HttpPost("register")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		public async Task<ActionResult<JwtResponse>> Register([FromBody] RegisterRequest request)
		{
			User user = request.ToUser();
			user.Password = BCryptNet.HashPassword(request.Password);
			try
			{
				await _users.Create(user);
			}
			catch (DuplicateField)
			{
				return Conflict(new { Message = "A user already exists with this username." });
			}


			return new JwtResponse
			{
				AccessToken = _token.CreateAccessToken(user, out DateTime expireDate),
				RefreshToken = await _token.CreateRefreshToken(user),
				ExpireTime = expireDate
			};
		}

		/// <summary>
		/// Refresh a token.
		/// </summary>
		/// <remarks>
		/// Refresh an access token using the given refresh token. A new access and refresh token are generated.
		/// The old refresh token should not be used anymore.
		/// </remarks>
		/// <param name="token">A valid refresh token.</param>
		/// <returns>A new access and refresh token.</returns>
		/// <response code="400">The given refresh token is invalid.</response>
		[HttpGet("refresh")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<JwtResponse>> Refresh([FromQuery] string token)
		{
			try
			{
				int userId = _token.GetRefreshTokenUser(token);
				User user = await _users.GetUserById(userId);
				return new JwtResponse
				{
					AccessToken = _token.CreateAccessToken(user, out DateTime expireDate),
					RefreshToken = await _token.CreateRefreshToken(user),
					ExpireTime = expireDate
				};
			}
			catch (SecurityTokenException ex)
			{
				return BadRequest(new { ex.Message });
			}
		}
	}
}
