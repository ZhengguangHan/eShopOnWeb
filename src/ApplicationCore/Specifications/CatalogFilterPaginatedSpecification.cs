using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;

public class CatalogFilterPaginatedSpecification : Specification<CatalogItem>
{
    public CatalogFilterPaginatedSpecification(int skip, int take, int? brandId, int? typeId)
        : this(skip, take, brandId, typeId, null)
    {
    }

    public CatalogFilterPaginatedSpecification(int skip, int take, int? brandId, int? typeId, CatalogSortOption? sortOption)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }

        var filtered = Query
            .Where(i => (!brandId.HasValue || i.CatalogBrandId == brandId) &&
                (!typeId.HasValue || i.CatalogTypeId == typeId));

        ISpecificationBuilder<CatalogItem> ordered = sortOption switch
        {
            CatalogSortOption.PriceAsc => filtered.OrderBy(i => i.Price),
            CatalogSortOption.PriceDesc => filtered.OrderByDescending(i => i.Price),
            CatalogSortOption.NameAsc => filtered.OrderBy(i => i.Name),
            _ => filtered
        };

        ordered.Skip(skip).Take(take);
    }
}
