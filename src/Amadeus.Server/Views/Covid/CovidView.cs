using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Amadeus.Server.Controllers.Covid;
using Amadeus.Server.Models.Configuration;
using Amadeus.Server.Models.Convid;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Amadeus.Server.Views.Covid
{

	[ApiController]
	[Route("/covid")]
	public class CovidView : ControllerBase
	{

		private readonly CovidController _covidController;

		public CovidView(CovidController covidController)
		{
			_covidController = covidController;
		}

		[HttpGet("{countryCode:length(2,2)}")]
		public async Task<ActionResult<CovidData>> GetCovidInfoCity([NotNull] string countryCode)
		{
			try
			{
				return await _covidController.GetCovidInfo(countryCode);
			}
#pragma warning disable CA1031
			catch (Exception e)
#pragma warning restore CA1031
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet]
		public async Task<ActionResult<CovidTotal>> GetCovidInfoTotal()
		{
			try
			{
				return await _covidController.GetCovidInfoLatestTotal();
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
