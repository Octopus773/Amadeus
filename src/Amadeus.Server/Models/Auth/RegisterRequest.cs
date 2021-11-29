using System.ComponentModel.DataAnnotations;

namespace Amadeus.Server.Models
{
	/// <summary>
	/// A model only used on register requests.
	/// </summary>
	public class RegisterRequest
	{
		/// <summary>
		/// The user email address.
		/// </summary>
		[Required]
		[EmailAddress(ErrorMessage = "The email must be a valid email address")]
		public string Email { get; set; }

		/// <summary>
		/// The user's username.
		/// </summary>
		[Required]
		[MinLength(2, ErrorMessage = "The username must have less then {1} characters")]
		[MaxLength(20, ErrorMessage = "The username must have at least {1} characters")]
		public string Username { get; set; }

		/// <summary>
		/// The user's password.
		/// </summary>
		[Required]
		[MinLength(8, ErrorMessage = "The password must have at least {1} characters")]
		public string Password { get; set; }

		/// <summary>
		/// Convert this register request to a new <see cref="User"/> class.
		/// </summary>
		/// <returns>A user representing this request.</returns>
		public User ToUser()
		{
			return new User
			{
				Username = Username,
				Email = Email
			};
		}
	}
}
