using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Threading.Tasks;
using Amadeus.AniList.Models;
using Amadeus.Server.Controllers;
using Amadeus.Server.Controllers.AniList;
using Amadeus.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Amadeus.Server.Views.AniList
{
	[ApiController]
	[Route("anilist")]
	[SuppressMessage("Globalization", "CA1305", MessageId = "Specify IFormatProvider")]
	public class AniListView : ControllerBase
	{
		private readonly AniListService _aniList;
		private readonly IRepository<User> _users;

		public AniListView(AniListService aniList, IRepository<User> users)
		{
			_aniList = aniList;
			_users = users;
		}

		[HttpGet("watchlist/{user?}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<Anime[]>> GetWatchList(string user = null)
		{
			if (user == null)
			{
				if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int uid))
					return BadRequest(new {error = "User required if you are not logged in to anilist."});
				User authenticated = await _users.GetById(uid);
				return await _aniList.GetWatchList(authenticated.AnilistID);
			}

			if (int.TryParse(user, out int id))
				return await _aniList.GetWatchList(id);
			return await _aniList.GetWatchList(user);
		}
	}
}
