namespace Microsoft.eShopWeb.Web.ViewModels;

public class CatalogItemDetailViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? PictureUri { get; set; }
    public decimal Price { get; set; }
}
