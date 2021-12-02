using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Amadeus.Server.Models.Configuration;
using Amadeus.Server.Models.Convid;
using Microsoft.Extensions.Options;

namespace Amadeus.Server.Controllers.Covid
{
	public class CovidController
	{
		private readonly IOptions<CovidConfiguration> _config;

		public CovidController(IOptions<CovidConfiguration> config)
		{
			_config = config;
		}

		public async Task<CovidData> GetWeather(string countryCode)
		{
			using HttpClient client = new();
			countryCode = HttpUtility.UrlEncode(countryCode);

			HttpResponseMessage response;
			using (HttpRequestMessage requestMessage =
				new HttpRequestMessage(HttpMethod.Get, "https://covid-19-data.p.rapidapi.com/country/code"))
			{
				requestMessage.Headers.Add("x-rapidapi-host", _config.Value.ApiHost);
				requestMessage.Headers.Add("x-rapidapi-key", _config.Value.ApiKey);
				requestMessage.Content = new FormUrlEncodedContent(new Dictionary<string, string> {
						{"code", countryCode}
					});

				response = await client.SendAsync(requestMessage);
			}

			response.EnsureSuccessStatusCode();
			dynamic data = await response.Content.ReadAsAsync<ExpandoObject>();
			return new CovidData
			{
				Country = data[0].country,
				Code = data[0].code,
				Confirmed = data[0].confirmed,
				Recovered = data[0].recovered,
				Critical = data[0].critical,
				Deaths = data[0].deaths
			};
		}
	}
}
