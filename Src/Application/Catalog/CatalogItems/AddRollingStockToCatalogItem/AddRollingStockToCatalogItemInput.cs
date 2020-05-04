using System;
using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Input;

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
