using Microsoft.EntityFrameworkCore;
using Amadeus.Server.Models;

namespace Amadeus.Server.Data
{
	/// <summary>
	/// UserDB.
	/// </summary>
	public class UserDB : DbContext
	{
		/// <inheritdoc />
		public UserDB(DbContextOptions<UserDB> options)
			: base(options)
		{
		}

		public DbSet<User> Users { get; set; }
	}
}
