using Amadeus.Server.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Amadeus.Server.Views.Core
{
	[ApiController]
	[Route("/")]
	public class CoreView : ControllerBase
	{
		private readonly AboutController _aboutController;

		/// <summary>
		/// ctor.
		/// </summary>
		/// <param name="aboutController">The controller that gives the about.json.</param>
		public CoreView(AboutController aboutController)
		{
			_aboutController = aboutController;
		}


		/// <summary>
		/// deliver the about.json.
		/// </summary>
		/// <returns>The json.</returns>
		[HttpGet("about.json")]
		public object GetAbout()
		{
			return _aboutController.GetCurrentAboutJson(HttpContext.Connection.RemoteIpAddress?.ToString() ?? "???");
		}
	}
}
