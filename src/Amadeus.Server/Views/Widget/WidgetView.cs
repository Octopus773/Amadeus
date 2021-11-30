using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
			{
				return BadRequest("Invalid user credential");
			}
			return Ok(await _widgetRepository.GetWhere(w => w.UserId == userId));
		}

		[HttpPost]
		public async Task<ActionResult<Models.Widget>> CreateWidget([NotNull] WidgetCreationDTO widgetCreationDto)
		{
			if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
			{
				return BadRequest("Invalid user credential");
			}

			Models.Widget widget = new();
			widget.Parameters = widgetCreationDto.Parameters;
			widget.Type = widgetCreationDto.Type;
			widget.UserId = userId;
			return await _widgetRepository.Create(widget);
		}

		[HttpDelete("{uid:int}")]
		public async Task<ActionResult<Models.Widget>> DeleteWidget(int uid)
		{
			if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
			{
				return BadRequest("Invalid user credential");
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
		public async Task<ActionResult<Models.Widget>> ModifyWidget(int uid)
		{
			if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
			{
				return BadRequest("Invalid user credential");
			}

			try
			{
				return await _widgetRepository.Modify(uid);
			}
			catch (ElementNotFound e)
			{
				return NotFound(e.Message);
			}
		}
	}
}
