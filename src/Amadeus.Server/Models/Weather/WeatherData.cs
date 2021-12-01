namespace Amadeus.Server.Models.Weather
{
	public class WeatherData
	{
		/// <summary>
		/// The temperature in Celsius.
		/// </summary>
		public double Celsius { get; set; }

		/// <summary>
		/// The actual weather label.
		/// </summary>
		public string Weather { get; set; }

		/// <summary>
		/// The icon for the Weather Label.
		/// </summary>
		public string Icon { get; set; }
	}
}
