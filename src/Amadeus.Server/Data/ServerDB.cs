using Microsoft.EntityFrameworkCore;
using Amadeus.Server.Models;
using Namotion.Reflection;
using NJsonSchema.Annotations;

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

		public DbSet<Widget> Widgets { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Widget>()
				.Property(x => x.Parameters)
				.HasColumnType("jsonb");
		}
	}
}
