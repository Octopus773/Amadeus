using System.Collections.Generic;
using System.Threading.Tasks;
using Amadeus.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace Amadeus.Server.Views.Widget
{
	[ApiController]
	[Route("/widget")]
	public class WidgetView : ControllerBase
	{
		[HttpGet]
		public async Task<ActionResult<IList<Models.Widget>>> getWidgets()
		{
			return await Ok(new List<Models.Widget>());
		}
	}
}
