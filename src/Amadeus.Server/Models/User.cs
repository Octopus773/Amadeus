using System;
using System.ComponentModel.DataAnnotations;

namespace Amadeus.Server.Models
{
	/// <summary>
	/// The user of all the dashboard.
	/// </summary>
	public class User
	{
		/// <summary>
		/// The primary key for the db.
		/// </summary>
		public ulong Id { get; set; }

		/// <summary>
		/// User's username.
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// The name to use as a display.
		/// </summary>
		[Required]
		public string DisplayName { get; set; }

		/// <summary>
		/// The user's email
		/// </summary>
		[Required]
		public string Email { get; set; }

		/// <summary>
		/// The user's password.
		/// </summary>
		/// <note type="caution">
		/// It should encoded.
		/// </note>
		[Required]
		public string Password { get; set; }


		/// <summary>
		/// The creation time of the user.
		/// </summary>
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// True if the user is verified
		/// </summary>
		public bool Verified { get; set; }
	}
}
