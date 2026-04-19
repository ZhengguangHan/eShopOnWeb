using System.Net;
using System.Text.RegularExpressions;
using Reqnroll;
using WebTests.Support;

namespace WebTests.StepDefinitions;

[Binding]
public class CatalogSteps(WebContext context)
{
    [Given("the shopper is on the catalog detail page for item {string}")]
    public async Task GivenOnDetailPage(string id)
    {
        context.LastResponse = await context.Client.GetAsync($"/Catalog/{id}");
        context.LastResponse.EnsureSuccessStatusCode();
        context.LastBody = await context.LastResponse.Content.ReadAsStringAsync();
    }

    [When("the shopper submits the add-to-basket form on the detail page")]
    public async Task SubmitAddToBasket()
    {
        var token = WebPageHelpers.GetRequestVerificationToken(context.LastBody);
        var id = Regex.Match(context.LastBody, @"name=""id"" value=""(\d+)""").Groups[1].Value;

        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("id", id),
            new KeyValuePair<string, string>(WebPageHelpers.TokenTag, token)
        });
        context.LastResponse = await context.Client.PostAsync("/basket/index", content);
        context.LastResponse.EnsureSuccessStatusCode();
        context.LastBody = await context.LastResponse.Content.ReadAsStringAsync();
    }

    [Then("the response status should be {string}")]
    public void ThenResponseStatus(string expected)
    {
        var actual = context.LastResponse?.StatusCode.ToString() ?? string.Empty;
        Assert.AreEqual(expected, actual,
            $"Expected status \"{expected}\". Actual: \"{actual}\"");
    }

    [Then("the catalog page should show {string}")]
    public void ThenCatalogBodyContains(string expected)
    {
        Assert.IsTrue(context.LastBody.Contains(expected),
            $"Expected catalog response body to contain \"{expected}\".");
    }

    [Then("the catalog page should contain an image tag for catalog item {string}")]
    public void ThenCatalogContainsImage(string id)
    {
        var pattern = $"/images/products/{id}.png";
        Assert.IsTrue(context.LastBody.Contains(pattern),
            $"Expected catalog response body to contain image reference \"{pattern}\".");
    }

    [Then("the catalog page should contain a link to {string}")]
    public void ThenCatalogContainsLink(string href)
    {
        var pattern = $"href=\"{href}\"";
        Assert.IsTrue(context.LastBody.Contains(pattern),
            $"Expected catalog response body to contain link \"{pattern}\".");
    }

    [Then("the catalog page should not contain a direct add-to-basket form")]
    public void ThenCatalogHasNoInlineAddForm()
    {
        var markers = new[]
        {
            "action=\"/Basket/Index\"",
            "action=\"/basket/index\""
        };
        foreach (var marker in markers)
        {
            Assert.IsFalse(context.LastBody.Contains(marker),
                $"Expected home page body NOT to contain direct add-to-basket marker \"{marker}\".");
        }
    }
}
