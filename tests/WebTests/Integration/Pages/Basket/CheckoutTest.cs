using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebTests.Support;

namespace WebTests.Integration.Pages.Basket;

[TestClass]
[DoNotParallelize]
public class CheckoutTest
{
    private static readonly TestApplication _factory = new();
    private readonly HttpClient _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
    {
        AllowAutoRedirect = true
    });

    [TestMethod]
    public async Task SucessfullyPay()
    {
        var detail = await _client.GetAsync("/Catalog/2");
        detail.EnsureSuccessStatusCode();
        var detailBody = await detail.Content.ReadAsStringAsync();

        var addForm = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("id", "2"),
            new KeyValuePair<string, string>("name", "shirt"),
            new KeyValuePair<string, string>("price", "19.49"),
            new KeyValuePair<string, string>(WebPageHelpers.TokenTag, WebPageHelpers.GetRequestVerificationToken(detailBody))
        });
        var add = await _client.PostAsync("/basket/index", addForm);
        add.EnsureSuccessStatusCode();
        var addBody = await add.Content.ReadAsStringAsync();
        StringAssert.Contains(addBody, ".NET Black &amp; White Mug");

        var login = await _client.GetAsync("/Identity/Account/Login");
        var loginBody = await login.Content.ReadAsStringAsync();
        var loginForm = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("email", "demouser@microsoft.com"),
            new KeyValuePair<string, string>("password", "Pass@word1"),
            new KeyValuePair<string, string>(WebPageHelpers.TokenTag, WebPageHelpers.GetRequestVerificationToken(loginBody))
        });
        var loggedIn = await _client.PostAsync("/Identity/Account/Login?ReturnUrl=%2FBasket%2FCheckout", loginForm);
        var loggedInBody = await loggedIn.Content.ReadAsStringAsync();

        var checkoutForm = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Items[0].Id", "2"),
            new KeyValuePair<string, string>("Items[0].Quantity", "1"),
            new KeyValuePair<string, string>(WebPageHelpers.TokenTag, WebPageHelpers.GetRequestVerificationToken(loggedInBody))
        });
        var checkout = await _client.PostAsync("/basket/checkout", checkoutForm);
        var checkoutBody = await checkout.Content.ReadAsStringAsync();

        StringAssert.Contains(checkout.RequestMessage!.RequestUri!.ToString(), "/Basket/Success");
        StringAssert.Contains(checkoutBody, "Thanks for your Order!");
    }
}
