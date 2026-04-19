using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebTests.Support;

namespace WebTests.Integration.Controllers;

[TestClass]
[DoNotParallelize]
public class AccountControllerSignIn
{
    private static readonly TestApplication _factory = new();
    private readonly HttpClient _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
    {
        AllowAutoRedirect = false
    });

    [TestMethod]
    public async Task ReturnsSignInScreenOnGet()
    {
        var response = await _client.GetAsync("/identity/account/login");
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();

        StringAssert.Contains(body, "demouser@microsoft.com");
    }

    [TestMethod]
    public void RegexMatchesValidRequestVerificationToken()
    {
        var input = @"<input name=""__RequestVerificationToken"" type=""hidden"" value=""CfDJ8Obhlq65OzlDkoBvsSX0tgxFUkIZ_qDDSt49D_StnYwphIyXO4zxfjopCWsygfOkngsL6P0tPmS2HTB1oYW-p_JzE0_MCFb7tF9Ol_qoOg_IC_yTjBNChF0qRgoZPmKYOIJigg7e2rsBsmMZDTdbnGo"" /><input name=""RememberMe"" type=""hidden"" value=""false"" /></form>";
        var regex = new Regex(@"name=""__RequestVerificationToken"" type=""hidden"" value=""([-A-Za-z0-9+=/\\_]+?)""");
        var match = regex.Match(input);
        var group = match.Groups.Values.LastOrDefault();
        Assert.IsNotNull(group);
        Assert.IsTrue(group.Value.Length > 50);
    }

    [TestMethod]
    public async Task ReturnsFormWithRequestVerificationToken()
    {
        var response = await _client.GetAsync("/identity/account/login");
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();

        var token = WebPageHelpers.GetRequestVerificationToken(body);
        Assert.IsTrue(token.Length > 50);
    }

    [TestMethod]
    public async Task ReturnsSuccessfulSignInOnPostWithValidCredentials()
    {
        var getResponse = await _client.GetAsync("/identity/account/login");
        getResponse.EnsureSuccessStatusCode();
        var body = await getResponse.Content.ReadAsStringAsync();

        var form = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Email", "demouser@microsoft.com"),
            new KeyValuePair<string, string>("Password", "Pass@word1"),
            new KeyValuePair<string, string>(WebPageHelpers.TokenTag, WebPageHelpers.GetRequestVerificationToken(body))
        });

        var post = await _client.PostAsync("/identity/account/login", form);
        Assert.AreEqual(HttpStatusCode.Redirect, post.StatusCode);
        Assert.AreEqual(new Uri("/", UriKind.Relative), post.Headers.Location);
    }

    [TestMethod]
    public async Task UpdatePhoneNumberProfile()
    {
        var login = await _client.GetAsync("/identity/account/login");
        login.EnsureSuccessStatusCode();
        var loginBody = await login.Content.ReadAsStringAsync();
        var loginForm = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Email", "demouser@microsoft.com"),
            new KeyValuePair<string, string>("Password", "Pass@word1"),
            new KeyValuePair<string, string>(WebPageHelpers.TokenTag, WebPageHelpers.GetRequestVerificationToken(loginBody))
        });
        await _client.PostAsync("/identity/account/login", loginForm);

        var profile = await _client.GetAsync("/manage/my-account");
        profile.EnsureSuccessStatusCode();
        var profileBody = await profile.Content.ReadAsStringAsync();

        var updateForm = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Email", "demouser@microsoft.com"),
            new KeyValuePair<string, string>("PhoneNumber", "03656565"),
            new KeyValuePair<string, string>(WebPageHelpers.TokenTag, WebPageHelpers.GetRequestVerificationToken(profileBody))
        });
        var updated = await _client.PostAsync("/manage/my-account", updateForm);
        Assert.AreEqual(HttpStatusCode.Redirect, updated.StatusCode);

        var verify = await _client.GetAsync("/manage/my-account");
        var verifyBody = await verify.Content.ReadAsStringAsync();
        StringAssert.Contains(verifyBody, "03656565");
    }
}
