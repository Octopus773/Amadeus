using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Amadeus.Server.Controllers;
using Amadeus.Server.Exceptions;
using Amadeus.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Amadeus.Server.Views.Widget
{
	[ApiController]
	[Authorize]
	[Route("/widget")]
	public class WidgetView : ControllerBase
	{
		private readonly IRepository<Models.Widget> _widgetRepository;

		public WidgetView(IRepository<Models.Widget> widgetRepository)
		{
			_widgetRepository = widgetRepository;
		}

		[HttpGet]
		public async Task<ActionResult<IList<Models.Widget>>> GetWidgets()
		{
			if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
				return BadRequest("Invalid user credential");
			return Ok(await _widgetRepository.GetWhere(w => w.UserId == userId));
		}

		[HttpPost]
		public async Task<ActionResult<Models.Widget>> CreateWidget([NotNull] WidgetDTO widgetDto)
		{
			if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
				return BadRequest("Invalid user credential");
			if (widgetDto == null)
				return BadRequest("Widget information wasn't provided");

			Models.Widget widget = new()
			{
				Parameters = widgetDto.Parameters,
				Type = widgetDto.Type,
				UserId = userId
			};
			return await _widgetRepository.Create(widget);
		}

		[HttpDelete("{uid:int}")]
		public async Task<ActionResult<Models.Widget>> DeleteWidget(int uid)
		{
			if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
				return BadRequest("Invalid user credential");

			IList<Models.Widget> wList = await _widgetRepository.GetWhere(x => x.Id == uid && x.UserId == userId);
			if (!wList.Any())
			{
				return BadRequest("The widget don't exist or you don't have access to it");
			}
			try
			{
				return await _widgetRepository.Delete(uid);
			}
			catch (ElementNotFound e)
			{
				return NotFound(e.Message);
			}
		}

		[HttpPut("{uid:int}")]
		public async Task<ActionResult<Models.Widget>> ModifyWidget(int uid, [NotNull] WidgetDTO widgetDto)
		{
			if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
				return BadRequest("Invalid user credential");

			if (widgetDto == null)
				return BadRequest("Widget information wasn't provided");

			IList<Models.Widget> wList = await _widgetRepository.GetWhere(x => x.Id == uid && x.UserId == userId);
			if (!wList.Any())
			{
				return BadRequest("The widget don't exist or you don't have access to it");
			}

			wList[0].Parameters = widgetDto.Parameters;
			wList[0].Type = widgetDto.Type;

			try
			{
				return await _widgetRepository.Modify(uid, wList[0]);
			}
			catch (ElementNotFound e)
			{
				return NotFound(e.Message);
			}
		}
	}
}
