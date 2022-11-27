using Duende.IdentityServer.Models;
using Duende.IdentityServer;

namespace ScoutsPal.Services.ScoutsIdentityManagerAPI
{
    public class StaticDetails
    {
        /// <summary>
        /// Admin users and Normal Users
        /// </summary>
        public const string Admin = "Admin";
        public const string Standard = "Standard";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> {
                new ApiScope("scout", "Scout Server"),
                new ApiScope(name:"read", displayName:"Read your data"),
                new ApiScope(name:"write", displayName:"Write your data"),
                new ApiScope(name:"delete", displayName:"Delete your data"),
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId="client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"read", "write", "profile"},
                },
                new Client
                {
                    ClientId="scout",
                    //Include secret on a web config
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:7051/signin-oidc"},

                    PostLogoutRedirectUris = { "https://localhost:7051/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "scout"
                    }
                }
            };
    }
}
