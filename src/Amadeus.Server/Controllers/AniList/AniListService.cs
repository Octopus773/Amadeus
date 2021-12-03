using System.Collections.Generic;
using System.Dynamic;
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
		private readonly IRepository<User> _users;
		private readonly IHttpClientFactory _factory;
		private readonly IOptions<AniListOptions> _options;

		public AniListService(IRepository<User> users,
			IHttpClientFactory factory,
			IOptions<AniListOptions> options)
		{
			_users = users;
			_factory = factory;
			_options = options;
		}

		public async Task<User> LinkAccount(int? userID, string code)
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
			JwtToken token = await response.Content.ReadAsAsync<JwtToken>();

			if (userID != null)
			{
				User user = await _users.GetById(userID.Value);
				user.ExternalTokens["anilist"] = token;
				await _users.Modify(user.Id, user);
				return user;
			}
			else
			{
				User user = await _GetUser(token);
				await _users.Create(user);
				return user;
			}
		}

		private async Task<User> _GetUser(JwtToken token)
		{
			using HttpClient client = _factory.CreateClient();
			client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.AccessToken}");
			HttpResponseMessage response = await client.PostAsJsonAsync($"https://graphql.anilist.co/", new
			{
				query = @"
					Viewer
					{
						name
					}"
			});
			response.EnsureSuccessStatusCode();
			dynamic rep = await response.Content.ReadAsAsync<ExpandoObject>();
			return new User
			{
				Username = rep.data.viewer.name,
				ExternalTokens = new Dictionary<string, JwtToken>
				{
					["anilist"] = token
				}
			};
		}
	}
}
