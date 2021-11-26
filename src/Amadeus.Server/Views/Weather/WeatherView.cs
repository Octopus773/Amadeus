using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Amadeus.Server.Controllers.Weather;
using Amadeus.Server.Models.Weather;

namespace Amadeus.Server.Views.Weather
{
	[ApiController]
	[Route("/weather/{city}")]
	public class WeatherView : ControllerBase
	{

		private readonly WeatherController _weatherController;

		/// <summary>
		/// ctor.
		/// </summary>
		/// <param name="weatherController">The weather controller to get the weather.</param>
		public WeatherView(WeatherController weatherController)
		{
			_weatherController = weatherController;
		}

		/// <summary>
		/// Get the weather for a given city.
		/// </summary>
		/// <param name="city">The city to get the weather.</param>
		/// <returns>All the weather infos.</returns>
		[HttpGet]
		public async Task<WeatherData> GetCityWeather([NotNull] string city)
		{
			return await _weatherController.GetWeather(city);
		}
	}
}
