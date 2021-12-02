using System;
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

		public async Task<CovidData> GetCovidInfo(string countryCode)
		{
			using HttpClient client = new();
			countryCode = HttpUtility.UrlEncode(countryCode);

			Uri uri = new($"https://covid-19-data.p.rapidapi.com/country/code?code={countryCode}");
			client.DefaultRequestHeaders.Add("x-rapidapi-key", _config.Value.ApiKey);
			HttpResponseMessage response = await client.GetAsync(uri);

			response.EnsureSuccessStatusCode();
			ExpandoObject[] dataList = await response.Content.ReadAsAsync<ExpandoObject[]>();
			dynamic data = dataList[0];
			return new CovidData
			{
				Country = data.country,
				Code = data.code,
				Confirmed = data.confirmed,
				Recovered = data.recovered,
				Critical = data.critical,
				Deaths = data.deaths
			};
		}
	}
}
