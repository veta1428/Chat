using System.Security.Claims;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityModel;

namespace AuthServer.Data
{
    internal class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "oauthClient",
                    ClientName = "Example client application using client credentials",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret> {new Secret("SuperSecretPassword".Sha256())}, // change me!
                    AllowedScopes = new List<string> {"api1.read"}
                },
                new Client
                {
                    ClientId = "oidcClient",
                    ClientName = "Example Client Application",
                    ClientSecrets = new List<Secret> {new Secret("SuperSecretPassword".Sha256())}, // change me!
                    
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = new List<string> {"http://localhost:5089/signin-oidc"},
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "role",
                        "api1.read"
                    },

                    RequirePkce = true,
                    AllowPlainTextPkce = false
                }
            };
        }
    }

    internal class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource
                {
                    Name = "api1",
                    DisplayName = "API #1",
                    Description = "Allow the application to access API #1 on your behalf",
                    Scopes = new List<string> {"api1.read", "api1.write"},
                    ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())}, // change me!
                    UserClaims = new List<string> {"role"}
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope("api1.read", "Read Access to API #1"),
                new ApiScope("api1.write", "Write Access to API #1")
            };
        }
    }

    internal class Users
    {
        public static List<TestUserModel> Get()
        {
            return new List<TestUserModel>
            {
                new TestUserModel
                {
                    Password = "Secret_123!",
                    Claims = new List<TestClaimModel>
                    {
                        new TestClaimModel(JwtClaimTypes.Email, "janedoe@gmail.com"),
                        new TestClaimModel(JwtClaimTypes.GivenName, "Jane"),
                        new TestClaimModel(JwtClaimTypes.FamilyName, "Doe")
                    }
                },
                new TestUserModel
                {
                    Password = "Secret_123!",
                    Claims = new List<TestClaimModel>
                    {
                        new TestClaimModel(JwtClaimTypes.Email, "firstovich@gmail.com"),
                        new TestClaimModel(JwtClaimTypes.GivenName, "User1"),
                        new TestClaimModel(JwtClaimTypes.FamilyName, "Firstovich")
                    }
                },
                new TestUserModel
                {
                    Password = "Secret_123!",
                    Claims = new List<TestClaimModel>
                    {
                        new TestClaimModel(JwtClaimTypes.Email, "secondovich@gmail.com"),
                        new TestClaimModel(JwtClaimTypes.GivenName, "User2"),
                        new TestClaimModel(JwtClaimTypes.FamilyName, "Secondovich")
                    }
                }
            };
        }
    }

    public class TestUserModel
    {
        public string Password { get; set; } = string.Empty;

        public List<TestClaimModel> Claims { get; set; } = new List<TestClaimModel>();
    }

    public class TestClaimModel
    {
        public TestClaimModel(string type, string value)
        {
            Type = type;
            Value = value;
        }

        public string Type { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;
    }
}
