using System.Text;
using System.Text.Json;
using Microsoft.eShopWeb;
using Microsoft.eShopWeb.PublicApi.RoleManagementEndpoints;
using PublicApiTests.Support;
using Reqnroll;

namespace PublicApiTests.StepDefinitions;

[Binding]
public class RoleManagementSteps(ApiContext context, ScenarioContext scenarioContext)
{
    [When("I request the list of roles")]
    public async Task WhenIRequestTheListOfRoles()
    {
        context.Response = await context.Client.GetAsync("api/roles");
        context.ResponseBody = await context.Response.Content.ReadAsStringAsync();
    }

    [When("I create a role with name {string}")]
    public async Task WhenICreateARoleWithName(string name)
    {
        var request = new CreateRoleRequest { Name = name };
        var jsonContent = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json");

        context.Response = await context.Client.PostAsync("api/roles", jsonContent);
        context.ResponseBody = await context.Response.Content.ReadAsStringAsync();
    }

    [When("I get the first role id from the role list")]
    public async Task WhenIGetTheFirstRoleIdFromTheRoleList()
    {
        var response = await context.Client.GetAsync("api/roles");
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        var model = body.FromJson<RoleListResponse>();
        Assert.IsNotNull(model);
        Assert.IsTrue(model.Roles.Count > 0);
        scenarioContext["RoleId"] = model.Roles.First().Id;
    }

    [When("I request role by that id")]
    public async Task WhenIRequestRoleByThatId()
    {
        var roleId = (string)scenarioContext["RoleId"];
        context.Response = await context.Client.GetAsync($"api/roles/{roleId}");
        context.ResponseBody = await context.Response.Content.ReadAsStringAsync();
    }

    [When("I request role by id {string}")]
    public async Task WhenIRequestRoleById(string roleId)
    {
        context.Response = await context.Client.GetAsync($"api/roles/{roleId}");
        context.ResponseBody = await context.Response.Content.ReadAsStringAsync();
    }

    [When("I get the id of role {string}")]
    public async Task WhenIGetTheIdOfRole(string roleName)
    {
        var response = await context.Client.GetAsync("api/roles");
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        var model = body.FromJson<RoleListResponse>();
        Assert.IsNotNull(model);
        var role = model.Roles.FirstOrDefault(x => x.Name == roleName);
        Assert.IsNotNull(role, $"Role '{roleName}' not found");
        scenarioContext["RoleId"] = role.Id;
    }

    [When("I delete role by that id")]
    public async Task WhenIDeleteRoleByThatId()
    {
        var roleId = (string)scenarioContext["RoleId"];
        context.Response = await context.Client.DeleteAsync($"api/roles/{roleId}");
        context.ResponseBody = await context.Response.Content.ReadAsStringAsync();
    }

    [When("I delete role by id {string}")]
    public async Task WhenIDeleteRoleById(string roleId)
    {
        context.Response = await context.Client.DeleteAsync($"api/roles/{roleId}");
        context.ResponseBody = await context.Response.Content.ReadAsStringAsync();
    }

    [Then("the response should contain roles")]
    public void ThenTheResponseShouldContainRoles()
    {
        var model = context.ResponseBody.FromJson<RoleListResponse>();
        Assert.IsNotNull(model);
        Assert.IsNotNull(model.Roles);
        Assert.IsTrue(model.Roles.Count > 0);
    }

    [Then("the created role should have name {string}")]
    public void ThenTheCreatedRoleShouldHaveName(string name)
    {
        var model = context.ResponseBody.FromJson<CreateRoleResponse>();
        Assert.IsNotNull(model);
        Assert.AreEqual(name, model.Role.Name);
    }
}
