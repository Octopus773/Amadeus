using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace Amadeus.Server.Authentification
{
	public class IdentityServerConfig
	{
		public static IEnumerable<Client> GetClients()
		{
			return new List<Client>
			{
				new()
				{
					ClientId = "ConsoleAppClient",
					AllowedGrantTypes = GrantTypes.ClientCredentials,

					ClientSecrets =
					{
						new Secret("secret".Sha256())
					},
					AllowedScopes = { IdentityServerConstants.LocalApi.ScopeName, "testapi"}
				}
			};
		}

		public static IEnumerable<ApiResource> GetApiResources()
		{
			return new List<ApiResource>
			{
				new("testapi", "My Test API")
			};
		}

		public static IEnumerable<IdentityResource> GetIdentityResources()
		{
			return new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile()
			};
		}

		public static IEnumerable<ApiScope> GetApiScopes()
		{
			return new List<ApiScope>
			{
				new("testapi", "Friendly scope name")
			};
		}
	}
}
