using System;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Shared
{
    public sealed class CatalogItemRefsRepository : ICatalogItemRefsRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public CatalogItemRefsRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<CatalogItemRef?> GetCatalogItemAsync(Slug slug)
        {
            var results = await _unitOfWork.QueryAsync<CatalogItemRefDto>(
                GetCatalogItemQuery,
                new { slug = slug.Value });

            return results
                .GroupBy(it => new
                {
                    it.catalog_item_id,
                    it.item_number,
                    it.slug,
                    it.description,
                    it.brand_name
                })
                .Select(it => new CatalogItemRef(
                    new CatalogItemId(it.Key.catalog_item_id),
                    it.Key.slug,
                    it.Key.brand_name,
                    it.Key.item_number,
                    it.Key.description,
                    it.Select(c => EnumHelpers.RequiredValueFor<Category>(c.category))
                    ))
                .FirstOrDefault();
        }

        #region [ Query / Commands ]

        private const string GetCatalogItemQuery = @"
            SELECT ci.catalog_item_id, ci.slug, ci.description, ci.item_number, b.name AS brand_name, rs.category 
            FROM catalog_items AS ci
            JOIN brands AS b on ci.brand_id = b.brand_id
            LEFT JOIN rolling_stocks AS rs ON rs.catalog_item_id = ci.catalog_item_id
            WHERE ci.slug = @Slug;";

        #endregion
    }
}