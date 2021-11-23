using System.Collections.Generic;
using IdentityServer4.Models;

namespace Amadeus.Server.Authentification
{
	public class IdentityServerConfig
	{
		public static IEnumerable<Client> GetClients()
		{
			return new List<Client>();
		}

		public static IEnumerable<ApiResource> GetApiResources()
		{
			return new List<ApiResource>();
		}
	}
}
