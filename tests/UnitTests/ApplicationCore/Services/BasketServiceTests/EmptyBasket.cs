using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using NSubstitute;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.BasketServiceTests;

public class EmptyBasket
{
    private readonly string _buyerId = "Test buyerId";
    private readonly IRepository<Basket> _mockBasketRepo = Substitute.For<IRepository<Basket>>();
    private readonly IAppLogger<BasketService> _mockLogger = Substitute.For<IAppLogger<BasketService>>();

    [Fact]
    public async Task RemovesAllItemsFromLoadedBasket()
    {
        var basket = new Basket(_buyerId);
        basket.AddItem(1, 1.1m, 2);
        basket.AddItem(2, 2.2m, 3);
        _mockBasketRepo.FirstOrDefaultAsync(Arg.Any<BasketWithItemsSpecification>(), Arg.Any<CancellationToken>())
            .Returns(basket);
        var basketService = new BasketService(_mockBasketRepo, _mockLogger);

        await basketService.EmptyBasketAsync(1);

        Assert.Empty(basket.Items);
    }

    [Fact]
    public async Task InvokesBasketRepositoryUpdateAsyncOnce()
    {
        var basket = new Basket(_buyerId);
        basket.AddItem(1, 1.1m, 1);
        _mockBasketRepo.FirstOrDefaultAsync(Arg.Any<BasketWithItemsSpecification>(), Arg.Any<CancellationToken>())
            .Returns(basket);
        var basketService = new BasketService(_mockBasketRepo, _mockLogger);

        await basketService.EmptyBasketAsync(1);

        await _mockBasketRepo.Received(1).UpdateAsync(basket, Arg.Any<CancellationToken>());
    }
}
