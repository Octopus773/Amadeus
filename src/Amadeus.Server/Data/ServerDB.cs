
using Microsoft.EntityFrameworkCore;
using Amadeus.Server.Models;

namespace Amadeus.Server.Data
{
	/// <summary>
	/// UserDB.
	/// </summary>
	public class ServerDB : DbContext
	{
		/// <summary>
		/// 	Class containing all models needed for the server.
		/// </summary>
		/// <param name="options">Db options.</param>
		public ServerDB(DbContextOptions<ServerDB> options)
			: base(options)
		{
		}

		/// <summary>
		/// Users objects.
		/// </summary>
		public DbSet<User> Users { get; set; }

		public DbSet<WidgetModel> Widgets { get; set; }
	}
}
