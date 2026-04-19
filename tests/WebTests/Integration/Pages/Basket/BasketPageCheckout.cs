using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebTests.Support;

namespace WebTests.Integration.Pages.Basket;

[TestClass]
[DoNotParallelize]
public class BasketPageCheckout
{
    private static readonly TestApplication _factory = new();
    private readonly HttpClient _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
    {
        AllowAutoRedirect = true
    });

    [TestMethod]
    public async Task RedirectsToLoginIfNotAuthenticated()
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

        var empty = new FormUrlEncodedContent(Array.Empty<KeyValuePair<string, string>>());
        var checkout = await _client.PostAsync("/Basket/Checkout", empty);
        StringAssert.Contains(checkout!.RequestMessage!.RequestUri!.ToString(), "/Identity/Account/Login");
    }
}
