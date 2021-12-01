using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Amadeus.Server.Models
{
	public class WidgetModificationDTO
	{
		public string Type { get; set; }

		[Column(TypeName = "jsonb")]
		public Dictionary<string, object> Parameters { get; set; }

	}
}
