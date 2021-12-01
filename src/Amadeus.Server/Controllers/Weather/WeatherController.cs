using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Amadeus.Server.Models.Configuration;
using Amadeus.Server.Models.Weather;
using Microsoft.Extensions.Options;

namespace Amadeus.Server.Controllers.Weather
{
	public class WeatherController
	{
		private readonly IOptions<WeatherConfiguration> _config;

		public WeatherController(IOptions<WeatherConfiguration> config)
		{
			_config = config;
		}

		public async Task<WeatherData> GetWeather(string city)
		{
			using HttpClient client = new();
			city = HttpUtility.UrlEncode(city);
			Uri uri = new($"https://api.weatherapi.com/v1/current.json?key={_config.Value.ApiKey}&q={city}&aqi=no");
			HttpResponseMessage response = await client.GetAsync(uri);
			response.EnsureSuccessStatusCode();
			dynamic data = await response.Content.ReadAsAsync<ExpandoObject>();
			return new WeatherData
			{
				Celsius = data.current.temp_c,
				Weather = data.current.condition.text,
				Icon = data.current.condition.icon
			};
		}

		public async Task<IList<WeatherData>> GetForeCast(string city, int days)
		{
			using HttpClient client = new();
			city = HttpUtility.UrlEncode(city);

			Uri uri = new($"http://api.weatherapi.com/v1/forecast.json?key={_config.Value.ApiKey}&q={city}&days={days}&aqi=no&alerts=no");
			HttpResponseMessage response = await client.GetAsync(uri);
			response.EnsureSuccessStatusCode();
			dynamic data = await response.Content.ReadAsAsync<ExpandoObject>();
			List<WeatherData> wd = new();

			foreach (dynamic foreCastDay in data.forecast.forecastday)
			{
				wd.Add(
				new WeatherData
				{
					Celsius = foreCastDay.day.avgtemp_c,
					Weather = foreCastDay.day.condition.text,
					Icon = foreCastDay.day.condition.icon
				});
			}

			return wd;
		}
	}
}
