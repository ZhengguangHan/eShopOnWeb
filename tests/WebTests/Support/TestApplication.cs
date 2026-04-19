using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebTests.Support;

public class TestApplication : WebApplicationFactory<IBasketViewModelService>
{
    private readonly string _catalogDbName = $"WebReqnroll-Catalog-{Guid.NewGuid()}";
    private readonly string _identityDbName = $"WebReqnroll-Identity-{Guid.NewGuid()}";

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        builder.ConfigureServices(services =>
        {
            var descriptors = services.Where(d =>
                d.ServiceType == typeof(DbContextOptions<CatalogContext>) ||
                d.ServiceType == typeof(DbContextOptions<AppIdentityDbContext>))
                .ToList();

            foreach (var descriptor in descriptors)
            {
                services.Remove(descriptor);
            }

            services.AddScoped(sp => new DbContextOptionsBuilder<CatalogContext>()
                .UseInMemoryDatabase(_catalogDbName)
                .UseApplicationServiceProvider(sp)
                .Options);

            services.AddScoped(sp => new DbContextOptionsBuilder<AppIdentityDbContext>()
                .UseInMemoryDatabase(_identityDbName)
                .UseApplicationServiceProvider(sp)
                .Options);
        });

        return base.CreateHost(builder);
    }
}
