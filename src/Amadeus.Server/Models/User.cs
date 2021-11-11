using System;

namespace Amadeus.Server.Models
{
	public class User
	{
		public UInt64 uid {get; set; }
		public String username { get; set; }
		public String displayName { get; set; }
		public String email { get; set; }
		public String password { get; set; }
	}
}
