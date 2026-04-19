using Microsoft.eShopWeb;
using Microsoft.eShopWeb.PublicApi.RoleMembershipEndpoints;
using PublicApiTests.Support;
using Reqnroll;

namespace PublicApiTests.StepDefinitions;

[Binding]
public class RoleMembershipSteps(ApiContext context, ScenarioContext scenarioContext)
{
    [When("I request members of role {string}")]
    public async Task WhenIRequestMembersOfRole(string roleName)
    {
        context.Response = await context.Client.GetAsync($"api/roles/{roleName}/members");
        context.ResponseBody = await context.Response.Content.ReadAsStringAsync();
    }

    [When("I get a member of role {string}")]
    public async Task WhenIGetAMemberOfRole(string roleName)
    {
        var response = await context.Client.GetAsync($"api/roles/{roleName}/members");
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        var model = body.FromJson<GetRoleMembershipResponse>();
        Assert.IsNotNull(model);
        Assert.IsTrue(model.RoleMembers.Count > 0, $"No members found in role '{roleName}'");
        scenarioContext["UserId"] = model.RoleMembers.First().Id;
    }

    [When("I remove that user from that role")]
    public async Task WhenIRemoveThatUserFromThatRole()
    {
        var roleId = (string)scenarioContext["RoleId"];
        var userId = (string)scenarioContext["UserId"];
        context.Response = await context.Client.DeleteAsync($"api/roles/{roleId}/members/{userId}");
        context.ResponseBody = await context.Response.Content.ReadAsStringAsync();
    }

    [When("I remove user {string} from that role")]
    public async Task WhenIRemoveUserFromThatRole(string userId)
    {
        var roleId = (string)scenarioContext["RoleId"];
        context.Response = await context.Client.DeleteAsync($"api/roles/{roleId}/members/{userId}");
        context.ResponseBody = await context.Response.Content.ReadAsStringAsync();
    }

    [When("I remove user from role with invalid role id")]
    public async Task WhenIRemoveUserFromRoleWithInvalidRoleId()
    {
        // Use a valid user ID but invalid role ID
        var response = await context.Client.GetAsync("api/roles/Administrators/members");
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        var model = body.FromJson<GetRoleMembershipResponse>();
        var userId = model!.RoleMembers.First().Id;

        context.Response = await context.Client.DeleteAsync(
            $"api/roles/00000000-0000-0000-0000-000000000000/members/{userId}");
        context.ResponseBody = await context.Response.Content.ReadAsStringAsync();
    }

    [Then("the response should contain role members")]
    public void ThenTheResponseShouldContainRoleMembers()
    {
        var model = context.ResponseBody.FromJson<GetRoleMembershipResponse>();
        Assert.IsNotNull(model);
        Assert.IsTrue(model.RoleMembers.Count > 0);
    }

    [Then("the response should contain no role members")]
    public void ThenTheResponseShouldContainNoRoleMembers()
    {
        var model = context.ResponseBody.FromJson<GetRoleMembershipResponse>();
        Assert.IsNotNull(model);
        Assert.AreEqual(0, model.RoleMembers.Count);
    }
}
