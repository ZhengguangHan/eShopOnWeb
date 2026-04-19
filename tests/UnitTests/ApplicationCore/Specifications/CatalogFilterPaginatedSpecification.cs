using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Specifications;

public class CatalogFilterPaginatedSpecification
{
    [Fact]
    public void ReturnsAllCatalogItems()
    {
        var spec = new eShopWeb.ApplicationCore.Specifications.CatalogFilterPaginatedSpecification(0, 10, null, null);

        var result = spec.Evaluate(GetTestCollection());

        Assert.NotNull(result);
        Assert.Equal(4, result.ToList().Count);
    }

    [Fact]
    public void Returns2CatalogItemsWithSameBrandAndTypeId()
    {
        var spec = new eShopWeb.ApplicationCore.Specifications.CatalogFilterPaginatedSpecification(0, 10, 1, 1);

        var result = spec.Evaluate(GetTestCollection()).ToList();

        Assert.NotNull(result);
        Assert.Equal(2, result.ToList().Count);
    }

    [Fact]
    public void ReturnsItemsOrderedByPriceAscendingWhenSortIsPriceAsc()
    {
        var spec = new eShopWeb.ApplicationCore.Specifications.CatalogFilterPaginatedSpecification(
            0, 10, null, null, CatalogSortOption.PriceAsc);

        var result = spec.Evaluate(GetTestCollection()).ToList();

        Assert.Equal(new[] { 1.00m, 1.50m, 2.00m, 3.00m }, result.Select(i => i.Price));
    }

    [Fact]
    public void ReturnsItemsOrderedByPriceDescendingWhenSortIsPriceDesc()
    {
        var spec = new eShopWeb.ApplicationCore.Specifications.CatalogFilterPaginatedSpecification(
            0, 10, null, null, CatalogSortOption.PriceDesc);

        var result = spec.Evaluate(GetTestCollection()).ToList();

        Assert.Equal(new[] { 3.00m, 2.00m, 1.50m, 1.00m }, result.Select(i => i.Price));
    }

    [Fact]
    public void ReturnsItemsOrderedByNameAscendingWhenSortIsNameAsc()
    {
        var spec = new eShopWeb.ApplicationCore.Specifications.CatalogFilterPaginatedSpecification(
            0, 10, null, null, CatalogSortOption.NameAsc);

        var result = spec.Evaluate(GetUnorderedByNameCollection()).ToList();

        Assert.Equal(new[] { "Alpha", "Bravo", "Charlie", "Delta" }, result.Select(i => i.Name));
    }

    [Fact]
    public void PreservesExistingBehaviorWhenSortIsNull()
    {
        var spec = new eShopWeb.ApplicationCore.Specifications.CatalogFilterPaginatedSpecification(
            0, 10, null, null, null);

        var result = spec.Evaluate(GetTestCollection()).ToList();

        Assert.Equal(4, result.Count);
    }

    private List<CatalogItem> GetTestCollection()
    {
        var catalogItemList = new List<CatalogItem>();

        catalogItemList.Add(new CatalogItem(1, 1, "Item 1", "Item 1", 1.00m, "TestUri1"));
        catalogItemList.Add(new CatalogItem(1, 1, "Item 1.5", "Item 1.5", 1.50m, "TestUri1"));
        catalogItemList.Add(new CatalogItem(2, 2, "Item 2", "Item 2", 2.00m, "TestUri2"));
        catalogItemList.Add(new CatalogItem(3, 3, "Item 3", "Item 3", 3.00m, "TestUri3"));

        return catalogItemList;
    }

    private List<CatalogItem> GetUnorderedByNameCollection()
    {
        return new List<CatalogItem>
        {
            new(1, 1, "Charlie", "Charlie", 3.00m, "TestUri"),
            new(1, 1, "Alpha", "Alpha", 1.00m, "TestUri"),
            new(1, 1, "Delta", "Delta", 4.00m, "TestUri"),
            new(1, 1, "Bravo", "Bravo", 2.00m, "TestUri"),
        };
    }
}
