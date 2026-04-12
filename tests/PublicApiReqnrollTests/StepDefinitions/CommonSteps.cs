using System.Net;
using PublicApiReqnrollTests.Hooks;
using PublicApiReqnrollTests.Support;
using Reqnroll;

namespace PublicApiReqnrollTests.StepDefinitions;

[Binding]
public class CommonSteps(ApiContext context)
{
    [Given("I am an anonymous user")]
    public void GivenIAmAnAnonymousUser()
    {
        // Client is already created without auth in BeforeScenario
    }

    [Given("I am authenticated as an {string}")]
    public void GivenIAmAuthenticatedAsAn(string role)
    {
        var token = role.ToLowerInvariant() switch
        {
            "admin" => TestHooks.GetAdminUserToken(),
            "product manager" => TestHooks.GetProductManagerUserToken(),
            "normal user" => TestHooks.GetNormalUserToken(),
            _ => throw new ArgumentException($"Unknown role: {role}")
        };
        TestHooks.SetBearerToken(context.Client, token);
    }

    [Given("I am authenticated as a {string}")]
    public void GivenIAmAuthenticatedAsA(string role)
    {
        GivenIAmAuthenticatedAsAn(role);
    }

    [Then("the response status code should be {int}")]
    public void ThenTheResponseStatusCodeShouldBe(int statusCode)
    {
        Assert.AreEqual((HttpStatusCode)statusCode, context.Response.StatusCode);
    }
}
