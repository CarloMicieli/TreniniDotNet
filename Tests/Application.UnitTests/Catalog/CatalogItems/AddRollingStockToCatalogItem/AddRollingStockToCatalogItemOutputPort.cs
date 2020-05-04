using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Common;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.Catalog.CatalogItems.AddRollingStockToCatalogItem
{
    public class AddRollingStockToCatalogItemOutputPort : OutputPortTestHelper<AddRollingStockToCatalogItemOutput>, IAddRollingStockToCatalogItemOutputPort
    {
        private MethodInvocation<Slug> CatalogItemWasNotFoundMethod { set; get; }
        private MethodInvocation<Slug> RailwayWasNotFoundMethod { set; get; }

        public AddRollingStockToCatalogItemOutputPort()
        {
            CatalogItemWasNotFoundMethod = NewMethod<Slug>(nameof(CatalogItemWasNotFound));
            RailwayWasNotFoundMethod = NewMethod<Slug>(nameof(RailwayWasNotFound));
        }

        public void CatalogItemWasNotFound(Slug itemSlug)
        {
            CatalogItemWasNotFoundMethod = CatalogItemWasNotFoundMethod.Invoked(itemSlug);
        }

        public void RailwayWasNotFound(Slug railwaySlug)
        {
            RailwayWasNotFoundMethod = RailwayWasNotFoundMethod.Invoked(railwaySlug);
        }

        public void AssertCatalogItemWasNotFound(Slug expectedSlug) =>
            CatalogItemWasNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedSlug);

        public void AssertRailwayWasNotFound(Slug expectedSlug) =>
            RailwayWasNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedSlug);

        public override IEnumerable<IMethodInvocation> Methods
        {
            get
            {
                var methods = new List<IMethodInvocation>
                {
                    CatalogItemWasNotFoundMethod,
                    RailwayWasNotFoundMethod
                };

                return base.Methods.Concat(methods);
            }
        }
    }
}
