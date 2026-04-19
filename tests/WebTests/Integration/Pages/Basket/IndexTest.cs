using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebTests.Support;

namespace WebTests.Integration.Pages.Basket;

[TestClass]
[DoNotParallelize]
public class IndexTest
{
    private static readonly TestApplication _factory = new();
    private readonly HttpClient _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
    {
        AllowAutoRedirect = true
    });

    [TestMethod]
    public async Task OnPostUpdateTo50Successfully()
    {
        var home = await _client.GetAsync("/");
        home.EnsureSuccessStatusCode();
        var homeBody = await home.Content.ReadAsStringAsync();

        var addForm = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("id", "2"),
            new KeyValuePair<string, string>("name", "shirt"),
            new KeyValuePair<string, string>(WebPageHelpers.TokenTag, WebPageHelpers.GetRequestVerificationToken(homeBody))
        });
        var add = await _client.PostAsync("/basket/index", addForm);
        add.EnsureSuccessStatusCode();
        var addBody = await add.Content.ReadAsStringAsync();
        StringAssert.Contains(addBody, ".NET Black &amp; White Mug");

        var updateForm = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Items[0].Id", WebPageHelpers.GetFirstItemId(addBody)),
            new KeyValuePair<string, string>("Items[0].Quantity", "49"),
            new KeyValuePair<string, string>(WebPageHelpers.TokenTag, WebPageHelpers.GetRequestVerificationToken(addBody))
        });
        var update = await _client.PostAsync("/basket/update", updateForm);
        var updateBody = await update.Content.ReadAsStringAsync();

        StringAssert.Contains(update!.RequestMessage!.RequestUri!.ToString(), "/basket/update");
        var expectedTotal = 416.50M;
        StringAssert.Contains(updateBody, expectedTotal.ToString("N2"));
    }

    [TestMethod]
    public async Task OnPostUpdateTo0EmptyBasket()
    {
        var home = await _client.GetAsync("/");
        home.EnsureSuccessStatusCode();
        var homeBody = await home.Content.ReadAsStringAsync();

        var addForm = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("id", "2"),
            new KeyValuePair<string, string>("name", "shirt"),
            new KeyValuePair<string, string>(WebPageHelpers.TokenTag, WebPageHelpers.GetRequestVerificationToken(homeBody))
        });
        var add = await _client.PostAsync("/basket/index", addForm);
        add.EnsureSuccessStatusCode();
        var addBody = await add.Content.ReadAsStringAsync();
        StringAssert.Contains(addBody, ".NET Black &amp; White Mug");

        var updateForm = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Items[0].Id", WebPageHelpers.GetFirstItemId(addBody)),
            new KeyValuePair<string, string>("Items[0].Quantity", "0"),
            new KeyValuePair<string, string>(WebPageHelpers.TokenTag, WebPageHelpers.GetRequestVerificationToken(addBody))
        });
        var update = await _client.PostAsync("/basket/update", updateForm);
        var updateBody = await update.Content.ReadAsStringAsync();

        StringAssert.Contains(update!.RequestMessage!.RequestUri!.ToString(), "/basket/update");
        StringAssert.Contains(updateBody, "Basket is empty");
    }
}
