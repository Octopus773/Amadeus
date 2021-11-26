namespace Amadeus.Server.Models.Weather
{
	/// <summary>
	/// The model of City Weather Widget.
	/// </summary>
	/// <remarks>
	/// It stores the parameters of the widget for a user.
	/// </remarks>
	public class WeatherCityModel
	{
		/// <summary>
		/// uid for the table.
		/// </summary>
		public int id { get; set; }

		/// <summary>
		/// the city to look the weather.
		/// </summary>
		public string city { get; set; }
	}
}
