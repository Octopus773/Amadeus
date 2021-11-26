using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Amadeus.Server.Views.Weather
{
	[ApiController]
	[Route("/weather/{city}")]
	public class WeatherView : ControllerBase
	{
		/// <summary>
		/// Get the weather for a given city.
		/// </summary>
		/// <param name="city">The city to get the weather.</param>
		/// <returns>All the weather infos.</returns>
		[HttpGet]
		public async Task<ActionResult> GetCityWeather([NotNull] string city)
		{
			return Ok(city);
		}
	}
}
