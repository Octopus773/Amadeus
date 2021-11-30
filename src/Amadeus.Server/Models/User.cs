// Amadeus.
// Copyright (c) Amadeus.
//
// See AUTHORS.md and LICENSE file in the project root for full license information.
//
// Amadeus is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// any later version.
//
// Amadeus is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Kyoo. If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Amadeus.Server.Models
{
	public static class Permissions
	{
		public const string Overall = "Overall";
		public const string Admin = "Admin";
	}

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

		/// <summary>
		/// The list of permissions of the user.
		/// This is one of <see cref="Amadeus.Server.Models.Permissions.Overall"/>
		/// or <see cref="Amadeus.Server.Models.Permissions.Admin"/>.
		/// </summary>
		public string[] Permissions { get; set; }
	}
}
