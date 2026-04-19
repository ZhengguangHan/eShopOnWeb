using System.Text;
using System.Text.Json;
using Microsoft.eShopWeb;
using Microsoft.eShopWeb.PublicApi.UserManagementEndpoints;
using Microsoft.eShopWeb.PublicApi.UserManagementEndpoints.Models;
using PublicApiTests.Support;
using Reqnroll;

namespace PublicApiTests.StepDefinitions;

[Binding]
public class UserManagementSteps(ApiContext context, ScenarioContext scenarioContext)
{
    [When("I request the list of users")]
    public async Task WhenIRequestTheListOfUsers()
    {
        context.Response = await context.Client.GetAsync("api/users");
        context.ResponseBody = await context.Response.Content.ReadAsStringAsync();
    }

    [When("I create a user with username {string}")]
    public async Task WhenICreateAUserWithUsername(string username)
    {
        var request = new CreateUserRequest
        {
            User = new UserDto { UserName = username }
        };
        var jsonContent = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json");

        context.Response = await context.Client.PostAsync("api/users", jsonContent);
        context.ResponseBody = await context.Response.Content.ReadAsStringAsync();
    }

    [When("I create a user with missing username")]
    public async Task WhenICreateAUserWithMissingUsername()
    {
        var request = new CreateUserRequest
        {
            User = new UserDto()
        };
        var jsonContent = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json");

        context.Response = await context.Client.PostAsync("api/users", jsonContent);
        context.ResponseBody = await context.Response.Content.ReadAsStringAsync();
    }

    [When("I get the user id for username {string}")]
    public async Task WhenIGetTheUserIdForUsername(string username)
    {
        var response = await context.Client.GetAsync($"api/users/name/{username}");
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        var model = body.FromJson<GetUserResponse>();
        Assert.IsNotNull(model);
        scenarioContext["UserId"] = model.User.Id;
    }

    [When("I delete user by that id")]
    public async Task WhenIDeleteUserByThatId()
    {
        var userId = (string)scenarioContext["UserId"];
        context.Response = await context.Client.DeleteAsync($"api/users/{userId}");
        context.ResponseBody = await context.Response.Content.ReadAsStringAsync();
    }

    [When("I delete user by id {string}")]
    public async Task WhenIDeleteUserById(string userId)
    {
        context.Response = await context.Client.DeleteAsync($"api/users/{userId}");
        context.ResponseBody = await context.Response.Content.ReadAsStringAsync();
    }

    [When("I request roles for that user")]
    public async Task WhenIRequestRolesForThatUser()
    {
        var userId = (string)scenarioContext["UserId"];
        context.Response = await context.Client.GetAsync($"api/users/{userId}/roles");
        context.ResponseBody = await context.Response.Content.ReadAsStringAsync();
    }

    [When("I request roles for user id {string}")]
    public async Task WhenIRequestRolesForUserId(string userId)
    {
        context.Response = await context.Client.GetAsync($"api/users/{userId}/roles");
        context.ResponseBody = await context.Response.Content.ReadAsStringAsync();
    }

    [Then("the response should contain users")]
    public void ThenTheResponseShouldContainUsers()
    {
        var model = context.ResponseBody.FromJson<UserListResponse>();
        Assert.IsNotNull(model);
        Assert.IsNotNull(model.Users);
        Assert.IsTrue(model.Users.Count > 0);
    }

    [Then("the created user should have an id")]
    public void ThenTheCreatedUserShouldHaveAnId()
    {
        var model = context.ResponseBody.FromJson<CreateUserResponse>();
        Assert.IsNotNull(model);
        Assert.IsFalse(string.IsNullOrEmpty(model.UserId));
    }

    [Then("the user roles should contain {string}")]
    public void ThenTheUserRolesShouldContain(string roleName)
    {
        var model = context.ResponseBody.FromJson<GetUserRolesResponse>();
        Assert.IsNotNull(model);
        Assert.IsTrue(model.Roles.Contains(roleName),
            $"Expected role '{roleName}' not found in [{string.Join(", ", model.Roles)}]");
    }
}
