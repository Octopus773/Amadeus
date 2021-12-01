using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Amadeus.Server.Controllers.Weather;
using Amadeus.Server.Models.Weather;
using Microsoft.AspNetCore.Mvc;

namespace Amadeus.Server.Views.Weather
{
	[ApiController]
	[Route("/weather")]
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
		[HttpGet("{city}")]
		public async Task<ActionResult<WeatherData>> GetCityWeather([NotNull] string city)
		{
			try
			{
				return await _weatherController.GetWeather(city);
			}
#pragma warning disable CA1031
			catch (Exception e)
#pragma warning restore CA1031
			{
				return BadRequest(e.Message);
			}
		}

		/// <summary>
		/// Get the forecast for a given city.
		/// </summary>
		/// <param name="city">The city to get the weather.</param>
		/// <param name="days">The number of days to get the forecast.</param>
		/// <returns>All the weather infos.</returns>
		[HttpGet("{city}/{days:int}")]
		public async Task<ActionResult<IList<WeatherData>>> GetCityForecast([NotNull] string city, int days)
		{
			try
			{
				return Ok(await _weatherController.GetForeCast(city, days));
			}
#pragma warning disable CA1031
			catch (Exception e)
#pragma warning restore CA1031
			{
				return BadRequest(e.Message);
			}
		}
	}
}
