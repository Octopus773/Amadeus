using System;

namespace Amadeus.AniList.Models
{
	public class AniListOptions
	{
		public string ClientID { get; set; }

		public string ClientSecret { get; set; }

		public Uri RedirectUri { get; set; }
	}
}
