using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.eShopWeb;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using PublicApiTests.Support;
using Reqnroll;

namespace PublicApiTests.StepDefinitions;

[Binding]
public class CatalogItemSteps(ApiContext context)
{
    private ListPagedCatalogItemResponse? _storedCatalogResponse;

    [When("I request catalog items with page size {int}")]
    public async Task WhenIRequestCatalogItemsWithPageSize(int pageSize)
    {
        context.Response = await context.Client.GetAsync($"api/catalog-items?pageSize={pageSize}");
        context.ResponseBody = await context.Response.Content.ReadAsStringAsync();
    }

    [When("I request catalog items with page size {int} and page index {int}")]
    public async Task WhenIRequestCatalogItemsWithPageSizeAndPageIndex(int pageSize, int pageIndex)
    {
        context.Response = await context.Client.GetAsync(
            $"api/catalog-items?pageSize={pageSize}&pageIndex={pageIndex}");
        context.ResponseBody = await context.Response.Content.ReadAsStringAsync();
    }

    [When("I store the catalog items response")]
    public void WhenIStoreTheCatalogItemsResponse()
    {
        _storedCatalogResponse = context.ResponseBody.FromJson<ListPagedCatalogItemResponse>();
    }

    [When("I request catalog item with id {int}")]
    public async Task WhenIRequestCatalogItemWithId(int id)
    {
        context.Response = await context.Client.GetAsync($"api/catalog-items/{id}");
        context.ResponseBody = await context.Response.Content.ReadAsStringAsync();
    }

    [When("I create a catalog item with name {string} and price {decimal}")]
    public async Task WhenICreateACatalogItemWithNameAndPrice(string name, decimal price)
    {
        var request = new CreateCatalogItemRequest
        {
            CatalogBrandId = 1,
            CatalogTypeId = 2,
            Description = "test description",
            Name = name,
            Price = price
        };
        var jsonContent = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json");

        context.Response = await context.Client.PostAsync("api/catalog-items", jsonContent);
        context.ResponseBody = await context.Response.Content.ReadAsStringAsync();
    }

    [When("I delete catalog item with id {int}")]
    public async Task WhenIDeleteCatalogItemWithId(int id)
    {
        context.Response = await context.Client.DeleteAsync($"api/catalog-items/{id}");
        context.ResponseBody = await context.Response.Content.ReadAsStringAsync();
    }

    [Then("the response should contain at most {int} catalog items")]
    public void ThenTheResponseShouldContainAtMostCatalogItems(int maxCount)
    {
        var model = context.ResponseBody.FromJson<ListPagedCatalogItemResponse>();
        Assert.IsNotNull(model);
        Assert.IsNotNull(model.CatalogItems);
        Assert.IsTrue(model.CatalogItems.Count <= maxCount);
        Assert.IsTrue(model.CatalogItems.Count > 0);
    }

    [Then("the response should contain a catalog item")]
    public void ThenTheResponseShouldContainACatalogItem()
    {
        var model = context.ResponseBody.FromJson<GetByIdCatalogItemResponse>();
        Assert.IsNotNull(model);
        Assert.IsNotNull(model.CatalogItem);
        Assert.IsTrue(model.CatalogItem.Id > 0);
    }

    [Then("the created catalog item should have name {string}")]
    public void ThenTheCreatedCatalogItemShouldHaveName(string name)
    {
        var model = context.ResponseBody.FromJson<CreateCatalogItemResponse>();
        Assert.IsNotNull(model);
        Assert.AreEqual(name, model.CatalogItem.Name);
    }

    [Then("the created catalog item should have price {decimal}")]
    public void ThenTheCreatedCatalogItemShouldHavePrice(decimal price)
    {
        var model = context.ResponseBody.FromJson<CreateCatalogItemResponse>();
        Assert.IsNotNull(model);
        Assert.AreEqual(price, model.CatalogItem.Price);
    }
}
