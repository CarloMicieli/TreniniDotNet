using System;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.CatalogItems.AddRollingStockToCatalogItem
{
    public sealed class AddRollingStockToCatalogItemInput : IUseCaseInput
    {
        public AddRollingStockToCatalogItemInput(Slug slug, RollingStockInput rollingStock)
        {
            Slug = slug;
            RollingStock = rollingStock ?? throw new ArgumentNullException(nameof(rollingStock));
        }

        public Slug Slug { get; }
        public RollingStockInput RollingStock { get; }
    }
}
