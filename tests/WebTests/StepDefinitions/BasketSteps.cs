using Reqnroll;
using WebTests.Support;

namespace WebTests.StepDefinitions;

[Binding]
public class BasketSteps(WebContext context)
{
    [Given("the shopper has loaded the home page")]
    public async Task GivenTheShopperHasLoadedTheHomePage()
    {
        context.LastResponse = await context.Client.GetAsync("/");
        context.LastResponse.EnsureSuccessStatusCode();
        context.LastBody = await context.LastResponse.Content.ReadAsStringAsync();
    }

    [Given("the shopper added catalog item {string} named {string} to the basket")]
    [When("the shopper adds catalog item {string} named {string} to the basket")]
    public async Task AddItemToBasket(string id, string name)
    {
        var token = WebPageHelpers.GetRequestVerificationToken(context.LastBody);
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("id", id),
            new KeyValuePair<string, string>("name", name),
            new KeyValuePair<string, string>(WebPageHelpers.TokenTag, token)
        });
        context.LastResponse = await context.Client.PostAsync("/basket/index", content);
        context.LastResponse.EnsureSuccessStatusCode();
        context.LastBody = await context.LastResponse.Content.ReadAsStringAsync();
    }

    [When("the shopper clears the cart")]
    public async Task ClearTheCart()
    {
        var token = WebPageHelpers.GetRequestVerificationToken(context.LastBody);
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>(WebPageHelpers.TokenTag, token)
        });
        context.LastResponse = await context.Client.PostAsync("/basket/empty", content);
        context.LastBody = await context.LastResponse.Content.ReadAsStringAsync();
    }

    [When("the shopper updates the first item quantity to {string}")]
    public async Task UpdateFirstItemQuantity(string quantity)
    {
        var token = WebPageHelpers.GetRequestVerificationToken(context.LastBody);
        var itemId = WebPageHelpers.GetFirstItemId(context.LastBody);
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Items[0].Id", itemId),
            new KeyValuePair<string, string>("Items[0].Quantity", quantity),
            new KeyValuePair<string, string>(WebPageHelpers.TokenTag, token)
        });
        context.LastResponse = await context.Client.PostAsync("/basket/update", content);
        context.LastBody = await context.LastResponse.Content.ReadAsStringAsync();
    }

    [When("the shopper logs in as {string} with password {string} returning to {string}")]
    public async Task LogIn(string email, string password, string returnUrl)
    {
        var loginPage = await context.Client.GetAsync("/Identity/Account/Login");
        var loginBody = await loginPage.Content.ReadAsStringAsync();
        var token = WebPageHelpers.GetRequestVerificationToken(loginBody);
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Input.Email", email),
            new KeyValuePair<string, string>("Input.Password", password),
            new KeyValuePair<string, string>("email", email),
            new KeyValuePair<string, string>("password", password),
            new KeyValuePair<string, string>(WebPageHelpers.TokenTag, token)
        });
        var escaped = Uri.EscapeDataString(returnUrl);
        context.LastResponse = await context.Client.PostAsync($"/Identity/Account/Login?ReturnUrl={escaped}", content);
        context.LastBody = await context.LastResponse.Content.ReadAsStringAsync();
    }

    [When("the shopper posts to checkout")]
    public async Task PostCheckout()
    {
        var token = WebPageHelpers.GetRequestVerificationToken(context.LastBody);
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>(WebPageHelpers.TokenTag, token)
        });
        context.LastResponse = await context.Client.PostAsync("/Basket/Checkout", content);
        context.LastBody = await context.LastResponse.Content.ReadAsStringAsync();
    }

    [When("the shopper posts the first item to checkout with quantity {string}")]
    public async Task PostCheckoutWithFirstItem(string quantity)
    {
        var token = WebPageHelpers.GetRequestVerificationToken(context.LastBody);
        var itemId = WebPageHelpers.GetFirstItemId(context.LastBody);
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Items[0].Id", itemId),
            new KeyValuePair<string, string>("Items[0].Quantity", quantity),
            new KeyValuePair<string, string>(WebPageHelpers.TokenTag, token)
        });
        context.LastResponse = await context.Client.PostAsync("/Basket/Checkout", content);
        context.LastBody = await context.LastResponse.Content.ReadAsStringAsync();
    }

    [When("the shopper visits {string}")]
    public async Task VisitUrl(string url)
    {
        context.LastResponse = await context.Client.GetAsync(url);
        context.LastBody = await context.LastResponse.Content.ReadAsStringAsync();
    }

    [Then("the basket page should show {string}")]
    public void ThenBodyContains(string expected)
    {
        Assert.IsTrue(context.LastBody.Contains(expected),
            $"Expected response body to contain \"{expected}\". Actual length: {context.LastBody.Length}");
    }

    [Then("the basket page should not show {string}")]
    public void ThenBodyDoesNotContain(string unexpected)
    {
        Assert.IsFalse(context.LastBody.Contains(unexpected),
            $"Expected response body NOT to contain \"{unexpected}\".");
    }

    [Then("the basket should show quantity {string} for the first item")]
    public void ThenQuantityIs(string expected)
    {
        var marker = $"name=\"Items[0].Quantity\" value=\"{expected}\"";
        var altMarker = $"value=\"{expected}\" name=\"Items[0].Quantity\"";
        Assert.IsTrue(context.LastBody.Contains(marker) || context.LastBody.Contains(altMarker),
            $"Expected basket to show quantity {expected} for the first item.");
    }

    [Then("the last request URL should contain {string}")]
    public void ThenLastUrlContains(string fragment)
    {
        var url = context.LastResponse?.RequestMessage?.RequestUri?.ToString() ?? string.Empty;
        Assert.IsTrue(url.Contains(fragment, StringComparison.OrdinalIgnoreCase),
            $"Expected final URL to contain \"{fragment}\". Actual: {url}");
    }

    [Then("the last request URL should end with {string}")]
    public void ThenLastUrlEndsWith(string suffix)
    {
        var url = context.LastResponse?.RequestMessage?.RequestUri?.AbsolutePath ?? string.Empty;
        Assert.IsTrue(url.Equals(suffix, StringComparison.OrdinalIgnoreCase),
            $"Expected final URL path to equal \"{suffix}\". Actual: {url}");
    }
}
