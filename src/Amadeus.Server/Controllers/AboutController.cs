using System;
using Amadeus.Server.Controllers;

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
		public object GetCurrentAboutJson()
		{
			return new
			{
				client = new
				{
					host = Environment.GetEnvironmentVariable("AMADEUS_HOST") ?? UtilsController.GetLocalIpAddress()
				},
				server = new
				{
					current_time = (uint)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
					services = new[]
					{
						new
						{
							name = "weather",
							widgets = new[]
							{
								new
								{
									name = "city_temperature",
									description = "Display temperature for a city",
									paramsij = new[]
									{
										new
										{
											name = "city",
											type = "string"
										}
									}
								}
							}
						},
						new
						{
							name = "rss",
							widgets = new[]
							{
								new
								{
									name = "article_list",
									description = " Displaying the list of the last articles ",
									paramsij = new[]
									{
										new
										{
											name = "link",
											type = "string"
										},
										new
										{
											name = "number",
											type = "integer"
										}
									}
								}
							}
						}
					}
				}
			};
		}
	}
}
