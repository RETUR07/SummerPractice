using IdentityServer4.Models;
using System.Collections.Generic;

namespace SocialNetwork.IdentityTokenServer
{
    internal static class ClientManager
    {
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    AllowOfflineAccess = true,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "SocialNetwork" }
                }
            };
    }
}
