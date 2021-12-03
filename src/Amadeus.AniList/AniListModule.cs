using System.Diagnostics.CodeAnalysis;
using Amadeus.AniList.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Amadeus.AniList
{
	public static class AniListModule
	{
		public static void ConfigureServices(IServiceCollection services, [NotNull] IConfiguration configuration)
		{
			services.Configure<AniListOptions>(configuration.GetSection("anilist"));
		}
	}
}
