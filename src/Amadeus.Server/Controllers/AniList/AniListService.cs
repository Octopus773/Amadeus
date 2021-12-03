using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Amadeus.AniList.Models;
using Amadeus.Server.Data;
using Amadeus.Server.Models;
using Microsoft.Extensions.Options;

namespace Amadeus.Server.Controllers.AniList
{
	public class AniListService
	{
		private readonly IRepository<User> _user;
		private readonly IHttpClientFactory _factory;
		private readonly IOptions<AniListOptions> _options;

		public AniListService(IRepository<User> user,
			IHttpClientFactory factory,
			IOptions<AniListOptions> options)
		{
			_user = user;
			_factory = factory;
			_options = options;
		}

		public async Task LinkAccount(int? userID, string code)
		{
			using HttpClient client = _factory.CreateClient();
			HttpResponseMessage response = await client.PostAsJsonAsync($"https://anilist.co/api/v2/oauth/token", new
			{
				grant_type = "authorization_code",
				client_id = _options.Value.ClientID,
				client_secret = _options.Value.ClientSecret,
				redirect_uri = _options.Value.RedirectUri,
				code
			});
			response.EnsureSuccessStatusCode();

			if (userID == null)
				return;
			else
			{

			}
		}
	}
}
