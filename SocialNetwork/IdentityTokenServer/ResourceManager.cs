
using IdentityServer4.Models;
using System.Collections.Generic;

namespace SocialNetwork.IdentityTokenServer
{
    internal static class ResourceManager
    {
        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource {
                    Name = "SocialNetwork",
                    DisplayName = "SocialNetwork Api",
                    ApiSecrets = { new Secret("SocialNetwork".Sha256()) },
                    Scopes = new List<string> {
                        "SocialNetwork"
                    }
                },
            };
    }
}
