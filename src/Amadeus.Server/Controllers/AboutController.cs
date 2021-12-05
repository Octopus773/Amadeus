using System;

namespace Amadeus.Server.Controllers
{
	/// <summary>
	/// Controller used to serve the about.json.
	/// </summary>
	public class AboutController
	{
		/// <summary>
		/// Generate the current about.json.
		/// </summary>
		/// <returns>The C# object that represents the about.json.</returns>
		public object GetCurrentAboutJson(string address)
		{
			return new
			{
				client = new
				{
					host = address
				},
				server = new
				{
					current_time = (uint) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
					services = new[]
					{
						new
						{
							name = "weather",
							widgets = new[]
							{
								new
								{
									name = "city_weather",
									description = "Display temperature & weather for a city",
									@params = new[]
									{
										new
										{
											name = "city",
											type = "string"
										}
									}
								},
								new
								{
									name = "forecast",
									description = "Display temperature & weather for the next 1 to 3 days for a city",
									@params = new[]
									{
										new
										{
											name = "city",
											type = "string"
										},
										new
										{
											name = "days",
											type = "integer"
										}
									}
								}
							}
						},
						new
						{
							name = "covid",
							widgets = new[]
							{
								new
								{
									name = "covid_information",
									description = "Displaying the list number of confirmed, critical, death and recovered cases of covid in one country or globally.",
									@params = new[]
									{
										new
										{
											name = "country_code",
											type = "string"
										}
									}
								}
							}
						},
						new
						{
							name = "anilist",
							widgets = new[]
							{
								new
								{
									name = "watchlist",
									description = "Displaying the watchlist of the currently authenticated user or any other user by ID or name.",
									@params = new[]
									{
										new
										{
											name = "user",
											type = "string"
										},
										new
										{
											name = "title",
											type = "string"
										}
									}
								},
								new
								{
									name = "anime_description",
									description = "Search for an anime and display information about this show.",
									@params = new[]
									{
										new
										{
											name = "query",
											type = "string"
										},
										new
										{
											name = "title",
											type = "string"
										}
									}
								},
								new
								{
									name = "anime_trends",
									description = "Display a list of show trending now.",
									@params = new[]
									{
										new
										{
											name = "type",
											type = "string"
										},
										new
										{
											name = "title",
											type = "string"
										}
									}
								},
							}
						}
					}
				}
			};
		}
	}
}
