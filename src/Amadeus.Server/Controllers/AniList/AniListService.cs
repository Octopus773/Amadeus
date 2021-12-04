using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
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

		private async Task<JwtToken> _GetToken(string code)
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
			return await response.Content.ReadAsAsync<JwtToken>();
		}

		private async Task<User> _GetUser(JwtToken token)
		{
			using HttpClient client = _factory.CreateClient();
			client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.AccessToken}");
			HttpResponseMessage response = await client.PostAsJsonAsync($"https://graphql.anilist.co/", new
			{
				query = @"{
					Viewer
					{
						id
						name
					}
				}"
			});
			response.EnsureSuccessStatusCode();
			dynamic rep = await response.Content.ReadAsAsync<ExpandoObject>();
			return new User
			{
				AnilistID = rep.data.Viewer.id,
				Username = rep.data.Viewer.name,
				ExternalTokens = new Dictionary<string, JwtToken>
				{
					["anilist"] = token
				}
			};
		}

		public async Task<User> Login(string code)
		{
			JwtToken token = await _GetToken(code);
			User aniListUser = await _GetUser(token);
			User existing = (await _users.GetWhere(x => x.AnilistID == aniListUser.AnilistID)).FirstOrDefault();
			if (existing != null)
				return existing;
			await _users.Create(aniListUser);
			return aniListUser;
		}

		public async Task<User> LinkAccount(int userID, string code)
		{
			JwtToken token = await _GetToken(code);
			User user = await _users.GetById(userID);
			User aniList = await _GetUser(token);
			user.AnilistID = aniList.AnilistID;
			user.ExternalTokens["anilist"] = token;
			await _users.Modify(user.Id, user);
			return user;
		}

		public async Task<Anime[]> GetWatchList(long user)
		{
			using HttpClient client = _factory.CreateClient();
			HttpResponseMessage response = await client.PostAsJsonAsync($"https://graphql.anilist.co/", new
			{
				query = @"
					query ($userID: Int) {
					  MediaListCollection(userId: $userID, status:CURRENT, type: ANIME) {
					    lists {
					      name
					      entries {
					        media {
					          title {
					            romaji
					            english
					            native
					          }
					          coverImage {
					             medium
					          }
					        }
					      }
					    }
					  }
					}",
				variables = new
				{
					userID = user
				}
			});
			response.EnsureSuccessStatusCode();
			dynamic rep = await response.Content.ReadAsAsync<ExpandoObject>();
			IEnumerable<dynamic> animes = rep.data.MediaListCollection.lists[0].entries;
			return animes.Select(x => new Anime
			{
				Title = new Title
				{
					English = x.media.title.english,
					Romaji = x.media.title.romaji,
					Native = x.media.title.native,
				},
				Image = x.media.coverImage.medium
			}).ToArray();
		}

		public async Task<Anime[]> GetWatchList(string user)
		{
			using HttpClient client = _factory.CreateClient();
			HttpResponseMessage response = await client.PostAsJsonAsync($"https://graphql.anilist.co/", new
			{
				query = @"
					query ($user: String) {
					  MediaListCollection(userName: $user, status:CURRENT, type: ANIME) {
					    lists {
					      name
					      entries {
					        media {
					          title {
					            romaji
					            english
					            native
					          }
					          coverImage {
					             medium
					          }
					        }
					      }
					    }
					  }
					}",
				variables = new
				{
					user
				}
			});
			response.EnsureSuccessStatusCode();
			dynamic rep = await response.Content.ReadAsAsync<ExpandoObject>();
			IEnumerable<dynamic> animes = rep.data.MediaListCollection.lists[0].entries;
			return animes.Select(x => new Anime
			{
				Title = new Title
				{
					English = x.media.title.english,
					Romaji = x.media.title.romaji,
					Native = x.media.title.native,
				},
				Image = x.media.coverImage.medium
			}).ToArray();
		}

		public async Task<Anime> GetAnime(string name)
		{
			using HttpClient client = _factory.CreateClient();
			HttpResponseMessage response = await client.PostAsJsonAsync($"https://graphql.anilist.co/", new
			{
				query = @"
					query($name: String) {
					  Media(search: $name) {
					    title {
					      romaji
					      english
					      native
					    }
					    description
					    bannerImage
					  }
					}",
				variables = new
				{
					name
				}
			});
			response.EnsureSuccessStatusCode();
			dynamic rep = await response.Content.ReadAsAsync<ExpandoObject>();
			dynamic x = rep.data.Media;
			return new Anime
			{
				Title = new Title
				{
					English = x.title.english,
					Romaji = x.title.romaji,
					Native = x.title.native,
				},
				Description = x.description,
				Image = x.bannerImage
			};
		}
	}
}
