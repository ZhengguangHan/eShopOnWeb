using System.Text;
using System.Text.Json;
using Microsoft.eShopWeb;
using Microsoft.eShopWeb.PublicApi.AuthEndpoints;
using PublicApiReqnrollTests.Support;
using Reqnroll;

namespace PublicApiReqnrollTests.StepDefinitions;

[Binding]
public class AuthenticationSteps(ApiContext context)
{
    [When("I authenticate with username {string} and password {string}")]
    public async Task WhenIAuthenticateWithUsernameAndPassword(string username, string password)
    {
        var request = new AuthenticateRequest
        {
            Username = username,
            Password = password
        };
        var jsonContent = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json");

        context.Response = await context.Client.PostAsync("api/authenticate", jsonContent);
        context.ResponseBody = await context.Response.Content.ReadAsStringAsync();
    }

    [Then("the authentication result should be true")]
    public void ThenTheAuthenticationResultShouldBeTrue()
    {
        var model = context.ResponseBody.FromJson<AuthenticateResponse>();
        Assert.IsNotNull(model);
        Assert.IsTrue(model.Result);
    }

    [Then("the authentication result should be false")]
    public void ThenTheAuthenticationResultShouldBeFalse()
    {
        var model = context.ResponseBody.FromJson<AuthenticateResponse>();
        Assert.IsNotNull(model);
        Assert.IsFalse(model.Result);
    }

    [Then("the response should contain a token")]
    public void ThenTheResponseShouldContainAToken()
    {
        var model = context.ResponseBody.FromJson<AuthenticateResponse>();
        Assert.IsNotNull(model);
        Assert.IsFalse(string.IsNullOrEmpty(model.Token));
    }
}
