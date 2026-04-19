using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Web;
using Microsoft.eShopWeb.Web.Extensions;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.Web.Extensions.CacheHelpersTests;

public class GenerateCatalogItemCacheKey
{
    [Fact]
    public void ReturnsCatalogItemCacheKeyWithoutSort()
    {
        var pageIndex = 0;
        int? brandId = null;
        int? typeId = null;
        CatalogSortOption? sortOption = null;

        var result = CacheHelpers.GenerateCatalogItemCacheKey(pageIndex, Constants.ITEMS_PER_PAGE, brandId, typeId, sortOption);

        Assert.Equal("items-0-10---", result);
    }

    [Fact]
    public void ReturnsCatalogItemCacheKeyWithSort()
    {
        var result = CacheHelpers.GenerateCatalogItemCacheKey(0, Constants.ITEMS_PER_PAGE, null, null, CatalogSortOption.PriceAsc);

        Assert.Equal("items-0-10---PriceAsc", result);
    }
}
