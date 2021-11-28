using System.ComponentModel.DataAnnotations;

namespace Amadeus.Server.Models
{
	/// <summary>
	/// A login request to retrieve an access and a refresh token.
	/// </summary>
	public class LoginRequest
	{
		/// <summary>
		/// The username of the user to log.
		/// </summary>
		[MinLength(2, ErrorMessage = "The username must have less then {1} characters")]
		[MaxLength(20, ErrorMessage = "The username must have at least {1} characters")]
		[Required]
		public string Username { get; set; }

		/// <summary>
		/// The password of the user.
		/// </summary>
		[Required]
		public string Password { get; set; }
	}
}
