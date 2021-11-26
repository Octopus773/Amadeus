namespace Amadeus.Server.Models.Weather
{
	public enum Weather
	{
		Rainy,
		Sunny,
		Cloudy
	}

	public class WeatherData
	{
		public int Degree { get; set; }
		public Weather Weather { get; set; }
	}
}
