namespace Amadeus.AniList.Models
{
	public class Anime
	{
		public Title Title { get; set; }

		public string Image { get; set; }

		public string Description { get; set; }
	}

	public class Title
	{
		public string Romaji { get; set; }

		public string English { get; set; }

		public string Native { get; set; }
	}
}
