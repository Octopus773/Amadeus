namespace Amadeus.Server.Models.Convid
{
	public class CovidData
	{
		public string Country { get; set; }

		public string Code { get; set; }

		public int Confirmed { get; set; }

		public int Recovered { get; set; }

		public int Critical { get; set; }

		public int Deaths { get; set; }
	}
}
