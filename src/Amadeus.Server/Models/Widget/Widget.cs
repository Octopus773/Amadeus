using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Amadeus.Server.Models
{
	public class Widget
	{
		public int Id { get; set; }

		[Required]
		public string Type { get; set; }

		[Required]
		[Column(TypeName = "jsonb")]
		public Dictionary<string, object> Parameters { get; set; }

		[Required]
		public int UserId { get; set; }
	}
}
