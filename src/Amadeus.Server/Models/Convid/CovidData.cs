namespace Amadeus.Server.Models.Convid
{
	public class CovidData
	{
		public string Country { get; set; }

		public string Code { get; set; }

		public long Confirmed { get; set; }

		public long Recovered { get; set; }

		public long Critical { get; set; }

		public long Deaths { get; set; }
	}
}
