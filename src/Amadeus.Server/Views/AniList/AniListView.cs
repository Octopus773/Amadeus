using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Threading.Tasks;
using Amadeus.AniList.Models;
using Amadeus.Server.Controllers.AniList;
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

		public AniListView(AniListService aniList)
		{
			_aniList = aniList;
		}

		[HttpGet("watchlist/{user?}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public Task<Anime[]> GetWatchList(string user = null)
		{
			if (user == null)
				return _aniList.GetWatchList(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
			if (int.TryParse(user, out int id))
				return _aniList.GetWatchList(id);
			return _aniList.GetWatchList(user);
		}
	}
}
