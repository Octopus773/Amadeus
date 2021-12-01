using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Amadeus.Server.Controllers;
using Amadeus.Server.Exceptions;
using Amadeus.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

		/// <summary>
		/// Get user widgets.
		/// </summary>
		/// <remarks>
		/// Get all the users registered widgets with theirs params.
		/// </remarks>
		/// <returns>A list of all the widgets.</returns>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IList<Models.Widget>>> GetWidgets()
		{
			if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
				return BadRequest("Invalid user credential");
			return Ok(await _widgetRepository.GetWhere(w => w.UserId == userId));
		}

		/// <summary>
		/// Create user widgets.
		/// </summary>
		/// <remarks>
		/// Create a user registered widget with its params.
		/// </remarks>
		/// <returns>The created widget.</returns>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
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

		/// <summary>
		/// Delete a user widget.
		/// </summary>
		/// <remarks>
		/// Delete a user registered widget.
		/// </remarks>
		/// <returns>The deleted widget.</returns>
		[HttpDelete("{uid:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<Models.Widget>> DeleteWidget(int uid)
		{
			if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
				return BadRequest("Invalid user credential");

			IList<Models.Widget> wList = await _widgetRepository.GetWhere(x => x.Id == uid && x.UserId == userId);
			if (!wList.Any())
			{
				return NotFound("The widget don't exist or you don't have access to it");
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

		/// <summary>
		/// Delete a user widget.
		/// </summary>
		/// <remarks>
		/// Delete a user registered widget.
		/// </remarks>
		/// <returns>The deleted widget.</returns>
		[HttpPut("{uid:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Models.Widget>> ModifyWidget(int uid, [NotNull] WidgetDTO widgetDto)
		{
			if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
				return BadRequest("Invalid user credential");

			if (widgetDto == null)
				return BadRequest("Widget information wasn't provided");

			IList<Models.Widget> wList = await _widgetRepository.GetWhere(x => x.Id == uid && x.UserId == userId);
			if (!wList.Any())
			{
				return NotFound("The widget don't exist or you don't have access to it");
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
