using System.ComponentModel.DataAnnotations;

namespace Amadeus.Server.Models
{
	/// <summary>
	/// Class used to gather data for the modification of a user.
	/// </summary>
	public class UserModificationDto
	{
		/// <summary>
		/// The name to use as a display.
		/// </summary>
		[MaxLength(30)] [MinLength(3)]
		public string Username { get; set; }

		/// <summary>
		/// The user's email.
		/// </summary>
		[EmailAddress] public string Email { get; set; }

		/// <summary>
		/// The user's password.
		/// </summary>
		/// <note type="caution">
		/// It should be encoded.
		/// </note>
		public string Password { get; set; }
	}
}
