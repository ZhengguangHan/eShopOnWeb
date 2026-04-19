using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ICatalogViewModelService _catalogViewModelService;

    public IndexModel(ICatalogViewModelService catalogViewModelService)
    {
        _catalogViewModelService = catalogViewModelService;
    }

    public required CatalogIndexViewModel CatalogModel { get; set; } = new CatalogIndexViewModel();

    public async Task OnGet(CatalogIndexViewModel catalogModel, int? pageId)
    {
        // Untrusted input: reject out-of-range enum values so they don't reach the
        // cache key or the query. Numeric values outside the defined set bind
        // successfully but would otherwise silently skip sorting and pollute cache.
        var sortApplied = catalogModel.SortApplied is { } value && Enum.IsDefined(value)
            ? catalogModel.SortApplied
            : null;

        CatalogModel = await _catalogViewModelService.GetCatalogItems(
            pageId ?? 0,
            Constants.ITEMS_PER_PAGE,
            catalogModel.BrandFilterApplied,
            catalogModel.TypesFilterApplied,
            sortApplied);
    }
}
