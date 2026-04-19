using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebTests.Support;

namespace WebTests.Integration.Controllers;

[TestClass]
[DoNotParallelize]
public class OrderIndexOnGet
{
    private static readonly TestApplication _factory = new();
    private readonly HttpClient _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
    {
        AllowAutoRedirect = false
    });

    [TestMethod]
    public async Task ReturnsRedirectGivenAnonymousUser()
    {
        var response = await _client.GetAsync("/order/my-orders");
        var redirectLocation = response!.Headers.Location!.OriginalString;

        Assert.AreEqual(HttpStatusCode.Redirect, response.StatusCode);
        StringAssert.Contains(redirectLocation, "/Account/Login");
    }
}
