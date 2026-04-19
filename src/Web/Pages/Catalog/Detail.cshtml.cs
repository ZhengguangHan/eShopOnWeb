using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Pages.Catalog;

public class DetailModel : PageModel
{
    private readonly IRepository<CatalogItem> _itemRepository;
    private readonly IUriComposer _uriComposer;

    public DetailModel(IRepository<CatalogItem> itemRepository, IUriComposer uriComposer)
    {
        _itemRepository = itemRepository;
        _uriComposer = uriComposer;
    }

    public CatalogItemDetailViewModel Item { get; set; } = new();

    public async Task<IActionResult> OnGet(int id)
    {
        var item = await _itemRepository.GetByIdAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        Item = new CatalogItemDetailViewModel
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            PictureUri = _uriComposer.ComposePicUri(item.PictureUri),
            Price = item.Price
        };

        return Page();
    }
}
