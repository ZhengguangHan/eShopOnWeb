using System.Text.RegularExpressions;
using Reqnroll;
using WebTests.Support;

namespace WebTests.StepDefinitions;

[Binding]
public class OrderDetailSteps(WebContext context)
{
    [Given("the shopper has placed an order for catalog item {string} named {string}")]
    public async Task PlaceOrder(string id, string name)
    {
        context.LastResponse = await context.Client.GetAsync("/");
        context.LastResponse.EnsureSuccessStatusCode();
        context.LastBody = await context.LastResponse.Content.ReadAsStringAsync();

        var addToken = WebPageHelpers.GetRequestVerificationToken(context.LastBody);
        var addContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("id", id),
            new KeyValuePair<string, string>("name", name),
            new KeyValuePair<string, string>(WebPageHelpers.TokenTag, addToken)
        });
        context.LastResponse = await context.Client.PostAsync("/basket/index", addContent);
        context.LastResponse.EnsureSuccessStatusCode();
        context.LastBody = await context.LastResponse.Content.ReadAsStringAsync();

        var loginPage = await context.Client.GetAsync("/Identity/Account/Login");
        var loginBody = await loginPage.Content.ReadAsStringAsync();
        var loginToken = WebPageHelpers.GetRequestVerificationToken(loginBody);
        var loginContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Input.Email", "demouser@microsoft.com"),
            new KeyValuePair<string, string>("Input.Password", "Pass@word1"),
            new KeyValuePair<string, string>("email", "demouser@microsoft.com"),
            new KeyValuePair<string, string>("password", "Pass@word1"),
            new KeyValuePair<string, string>(WebPageHelpers.TokenTag, loginToken)
        });
        context.LastResponse = await context.Client.PostAsync(
            "/Identity/Account/Login?ReturnUrl=" + Uri.EscapeDataString("/Basket/Checkout"),
            loginContent);
        context.LastBody = await context.LastResponse.Content.ReadAsStringAsync();

        var checkoutToken = WebPageHelpers.GetRequestVerificationToken(context.LastBody);
        var firstItemId = WebPageHelpers.GetFirstItemId(context.LastBody);
        var checkoutContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Items[0].Id", firstItemId),
            new KeyValuePair<string, string>("Items[0].Quantity", "1"),
            new KeyValuePair<string, string>(WebPageHelpers.TokenTag, checkoutToken)
        });
        context.LastResponse = await context.Client.PostAsync("/Basket/Checkout", checkoutContent);
        context.LastBody = await context.LastResponse.Content.ReadAsStringAsync();
    }

    [When("the shopper opens the detail page for their latest order")]
    public async Task OpenLatestOrderDetail()
    {
        var myOrders = await context.Client.GetAsync("/order/my-orders");
        myOrders.EnsureSuccessStatusCode();
        var myOrdersBody = await myOrders.Content.ReadAsStringAsync();

        var match = Regex.Match(myOrdersBody, @"/order/detail/(?<id>\d+)", RegexOptions.IgnoreCase);
        Assert.IsTrue(match.Success,
            $"Expected a /order/detail/{{id}} link on MyOrders. Body head: {myOrdersBody.Substring(0, Math.Min(800, myOrdersBody.Length))}");
        var orderId = match.Groups["id"].Value;

        context.LastResponse = await context.Client.GetAsync($"/order/detail/{orderId}");
        context.LastResponse.EnsureSuccessStatusCode();
        context.LastBody = await context.LastResponse.Content.ReadAsStringAsync();
    }

    [When("the shopper requests the orders stylesheet")]
    public async Task RequestOrdersStylesheet()
    {
        context.LastResponse = await context.Client.GetAsync("/css/orders/orders.component.css");
        context.LastResponse.EnsureSuccessStatusCode();
        context.LastBody = await context.LastResponse.Content.ReadAsStringAsync();
    }

    [Then("the order detail page should show {string}")]
    [Then("the orders stylesheet should contain {string}")]
    public void BodyContains(string expected)
    {
        Assert.IsTrue(context.LastBody.Contains(expected),
            $"Expected response body to contain \"{expected}\". Actual length: {context.LastBody.Length}");
    }

    [Then("the order detail page should not contain an image column with class {string}")]
    public void ImageColumnHasNoHiddenClass(string cssClass)
    {
        var pattern = $@"<section[^>]*class=""[^""]*\b{Regex.Escape(cssClass)}\b[^""]*""[^>]*>\s*<img[^>]*class=""esh-orders-detail-image""";
        Assert.IsFalse(Regex.IsMatch(context.LastBody, pattern, RegexOptions.IgnoreCase),
            $"Expected no <section class=\"... {cssClass} ...\"> wrapping .esh-orders-detail-image.");
    }
}
