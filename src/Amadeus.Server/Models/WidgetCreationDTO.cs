using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Amadeus.Server.Models
{
	public class WidgetCreationDTO
	{
		[Required]
		public string Type { get; set; }

		[Required]
		[Column(TypeName = "jsonb")]
		public Dictionary<string, object> Parameters { get; set; }

	}
}
