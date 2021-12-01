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
								}
							}
						},
						new
						{
							name = "forecast",
							widgets = new[]
							{
								new
								{
									name = "city_forecast",
									description = "Displaying the list of the next n days of weather for a city",
									@params = new[]
									{
										new
										{
											name = "city",
											type = "string"
										},
										new
										{
											name = "days in the future",
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
