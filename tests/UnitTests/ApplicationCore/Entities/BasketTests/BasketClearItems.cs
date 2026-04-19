using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Entities.BasketTests;

public class BasketClearItems
{
    private readonly string _buyerId = "Test buyerId";

    [Fact]
    public void RemovesAllItems()
    {
        var basket = new Basket(_buyerId);
        basket.AddItem(1, 1.1m, 1);
        basket.AddItem(2, 2.2m, 3);

        basket.ClearItems();

        Assert.Empty(basket.Items);
    }

    [Fact]
    public void OnEmptyBasketIsNoOp()
    {
        var basket = new Basket(_buyerId);

        basket.ClearItems();

        Assert.Empty(basket.Items);
    }
}
