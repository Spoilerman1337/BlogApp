using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace BlogApp.Auth.Application.Common.Identity;

public class IdentityConfiguration
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("BlogAppWebAPI", "Blog API"),
            new ApiScope("BlogAppAuthWebAPI", "Blog Auth API")
        };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            new ApiResource("BlogAppWebAPI", "Blog API", new[] { JwtClaimTypes.Name })
            {
                Scopes = { "BlogAppWebAPI" }
            },
            new ApiResource("BlogAppAuthWebAPI", "Blog Auth API", new[] { JwtClaimTypes.Name })
            {
                Scopes = { "BlogAppAuthWebAPI" }
            },
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "blog-app-web-api",
                ClientName = "Blog API",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                RequirePkce = true,
                RedirectUris =
                {
                    "http://localhost:5000/signin-oidc"
                },
                AllowedCorsOrigins = 
                {
                    "http://localhost:5000"
                },
                PostLogoutRedirectUris =
                {
                    "http://localhost:5000/signout-oidc"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "BlogAppWebAPI",
                    "BlogAppAuthWebAPI"
                },
                AllowAccessTokensViaBrowser = true
            }
        };
}
