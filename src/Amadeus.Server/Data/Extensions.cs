using System.Data.Common;
using Microsoft.Extensions.Configuration;

namespace Amadeus.Server.Data
{
	/// <summary>
	/// A class that regroup extensions used by some asp-net related parts of the app.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Get a connection string from the Configuration's section "Database".
		/// </summary>
		/// <param name="config">The IConfiguration instance to use.</param>
		/// <param name="database">The database's name.</param>
		/// <returns>A parsed connection string.</returns>
		public static string GetDatabaseConnection(this IConfiguration config)
		{
			DbConnectionStringBuilder builder = new();
			IConfigurationSection section = config.GetSection("database");
			foreach (IConfigurationSection child in section.GetChildren())
			{
				if (child.Key == "USER_ID")
					builder["USER ID"] = child.Value;
				else
					builder[child.Key] = child.Value;
			}

			return builder.ConnectionString;
		}
	}
}
