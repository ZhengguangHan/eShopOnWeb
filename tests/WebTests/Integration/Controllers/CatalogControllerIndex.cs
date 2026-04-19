using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebTests.Support;

namespace WebTests.Integration.Controllers;

[TestClass]
[DoNotParallelize]
public class CatalogControllerIndex
{
    private static readonly TestApplication _factory = new();
    private readonly HttpClient _client = _factory.CreateClient();

    [TestMethod]
    public async Task ReturnsHomePageWithProductListing()
    {
        var response = await _client.GetAsync("/");
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();

        StringAssert.Contains(body, ".NET Bot Black Sweatshirt");
    }
}
