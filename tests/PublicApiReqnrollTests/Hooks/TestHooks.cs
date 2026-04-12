using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.eShopWeb.ApplicationCore.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PublicApiReqnrollTests.Support;
using Reqnroll;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace PublicApiReqnrollTests.Hooks;

[Binding]
public class TestHooks
{
    private static WebApplicationFactory<Program> _application = default!;

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        _application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, config) =>
                {
                    config.AddInMemoryCollection(new Dictionary<string, string?>
                    {
                        ["UseOnlyInMemoryDatabase"] = "true"
                    });
                });
            });
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
        _application?.Dispose();
    }

    [BeforeScenario]
    public void BeforeScenario(ApiContext context)
    {
        context.Client = _application.CreateClient();
    }

    [AfterScenario]
    public void AfterScenario(ApiContext context)
    {
        context.Client?.Dispose();
        context.Response?.Dispose();
    }

    public static string GetAdminUserToken()
    {
        return CreateToken("admin@microsoft.com", ["Administrators"]);
    }

    public static string GetProductManagerUserToken()
    {
        return CreateToken("productmgr@microsoft.com", ["Product Managers"]);
    }

    public static string GetNormalUserToken()
    {
        return CreateToken("demouser@microsoft.com", []);
    }

    private static string CreateToken(string userName, string[] roles)
    {
        var claims = new List<Claim> { new(ClaimTypes.Name, userName) };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = Encoding.ASCII.GetBytes(AuthorizationConstants.JWT_SECRET_KEY);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims.ToArray()),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public static void SetBearerToken(HttpClient client, string token)
    {
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }
}
