using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.Catalog.CatalogItems.RemoveRollingStockFromCatalogItem
{
    public class RemoveRollingStockFromCatalogItemOutputPort : OutputPortTestHelper<RemoveRollingStockFromCatalogItemOutput>, IRemoveRollingStockFromCatalogItemOutputPort
    {
        private MethodInvocation<Slug, RollingStockId> RollingStockWasNotFoundMethod { set; get; }

        public RemoveRollingStockFromCatalogItemOutputPort()
        {
            RollingStockWasNotFoundMethod = NewMethod<Slug, RollingStockId>(nameof(RollingStockWasNotFound));
        }

        public void RollingStockWasNotFound(Slug slug, RollingStockId rollingStockId)
        {
            RollingStockWasNotFoundMethod = RollingStockWasNotFoundMethod.Invoked(slug, rollingStockId);
        }

        public void AssertRollingStockWasNotFound(Slug expectedSlug, RollingStockId expectedRollingStockId) =>
            RollingStockWasNotFoundMethod.ShouldBeInvokedWithTheArguments(expectedSlug, expectedRollingStockId);

        public override IEnumerable<IMethodInvocation> Methods
        {
            get
            {
                var methods = new List<IMethodInvocation>
                {
                    RollingStockWasNotFoundMethod
                };

                return base.Methods.Concat(methods);
            }
        }
    }
}
