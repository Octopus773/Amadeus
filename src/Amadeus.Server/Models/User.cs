using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;

namespace Amadeus.Server.Models
{
	/// <summary>
	/// The user of all the dashboard.
	/// </summary>
	[Index(nameof(Username), IsUnique = true)]
	[Index(nameof(Email), IsUnique = true)]
	public class User
	{
		/// <summary>
		/// The primary key for the db.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// User's username.
		/// </summary>
		[MaxLength(20)]
		[MinLength(2)]
		public string Username { get; set; }

		/// <summary>
		/// The name to use as a display.
		/// </summary>
		[Required]
		[MaxLength(30)]
		[MinLength(3)]
		public string DisplayName { get; set; }

		/// <summary>
		/// The user's email.
		/// </summary>
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		/// <summary>
		/// The user's password.
		/// </summary>
		/// <note type="caution">
		/// It should be encoded.
		/// </note>
		[Required]
		public string Password { get; set; }


		/// <summary>
		/// The creation time of the user.
		/// </summary>
		[Required]
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// True if the user is verified.
		/// </summary>
		[Required]
		public bool Verified { get; set; }
	}
}
