using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.Catalog.CatalogItems.EditRollingStock
{
    public class EditRollingStockOutputPort : OutputPortTestHelper<EditRollingStockOutput>, IEditRollingStockOutputPort
    {
        private MethodInvocation<Slug> RailwayWasNotFoundMethod { set; get; }
        private MethodInvocation<Slug, RollingStockId> RollingStockWasNotFoundMethod { get; set; }
        private MethodInvocation<Slug> CatalogItemWasNotFoundMethod { get; set; }

        public EditRollingStockOutputPort()
        {
            RailwayWasNotFoundMethod = NewMethod<Slug>(nameof(RailwayWasNotFound));
            RollingStockWasNotFoundMethod = NewMethod<Slug, RollingStockId>(nameof(RollingStockWasNotFound));
            CatalogItemWasNotFoundMethod = NewMethod<Slug>(nameof(CatalogItemWasNotFound));
        }

        public void CatalogItemWasNotFound(Slug slug)
        {
            CatalogItemWasNotFoundMethod = CatalogItemWasNotFoundMethod.Invoked(slug);
        }

        public void RailwayWasNotFound(Slug slug)
        {
            RailwayWasNotFoundMethod = RailwayWasNotFoundMethod.Invoked(slug);
        }

        public void RollingStockWasNotFound(Slug slug, RollingStockId rollingStockId)
        {
            RollingStockWasNotFoundMethod = RollingStockWasNotFoundMethod.Invoked(slug, rollingStockId);
        }

        public void AssertRailwayWasNotFound(Slug expectedSlug) =>
            RailwayWasNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedSlug);

        public void AssertRollingStockWasNotFound(Slug expectedSlug, RollingStockId expectedRollingStockId) =>
            RollingStockWasNotFoundMethod.ShouldBeInvokedWithTheArguments(expectedSlug, expectedRollingStockId);

        public void AssertCatalogItemWasNotFound(Slug expectedSlug) =>
            CatalogItemWasNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedSlug);

        public override IEnumerable<IMethodInvocation> Methods
        {
            get
            {
                var methods = new List<IMethodInvocation>
                {
                    RailwayWasNotFoundMethod,
                    RollingStockWasNotFoundMethod,
                    CatalogItemWasNotFoundMethod
                };

                return base.Methods.Concat(methods);
            }
        }
    }
}
