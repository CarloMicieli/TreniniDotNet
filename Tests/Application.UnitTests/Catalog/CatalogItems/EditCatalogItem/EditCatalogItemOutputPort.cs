using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Application.Catalog.CatalogItems.EditCatalogItem;
using TreniniDotNet.Common;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.InMemory.Catalog.CatalogItems.OutputPorts
{
    public class EditCatalogItemOutputPort : OutputPortTestHelper<EditCatalogItemOutput>, IEditCatalogItemOutputPort
    {
        private MethodInvocation<Slug> CatalogItemNotFoundMethod { set; get; }
        private MethodInvocation<Slug> BrandNotFoundMethod { set; get; }
        private MethodInvocation<Slug> ScaleNotFoundMethod { set; get; }
        private MethodInvocation<IEnumerable<Slug>> RailwayNotFoundMethod { set; get; }

        public EditCatalogItemOutputPort()
        {
            CatalogItemNotFoundMethod = NewMethod<Slug>(nameof(CatalogItemNotFound));
            BrandNotFoundMethod = NewMethod<Slug>(nameof(BrandNotFound));
            ScaleNotFoundMethod = NewMethod<Slug>(nameof(ScaleNotFound));
            RailwayNotFoundMethod = NewMethod<IEnumerable<Slug>>(nameof(RailwayNotFound));
        }

        public void CatalogItemNotFound(Slug slug)
        {
            CatalogItemNotFoundMethod = CatalogItemNotFoundMethod.Invoked(slug);
        }

        public void BrandNotFound(Slug brand)
        {
            BrandNotFoundMethod = BrandNotFoundMethod.Invoked(brand);
        }

        public void ScaleNotFound(Slug scale)
        {
            ScaleNotFoundMethod = ScaleNotFoundMethod.Invoked(scale);
        }

        public void RailwayNotFound(IEnumerable<Slug> railways)
        {
            RailwayNotFoundMethod = RailwayNotFoundMethod.Invoked(railways);
        }

        public override IEnumerable<IMethodInvocation> Methods
        {
            get
            {
                var methods = new List<IMethodInvocation>
                {
                    BrandNotFoundMethod,
                    CatalogItemNotFoundMethod,
                    ScaleNotFoundMethod,
                    RailwayNotFoundMethod
                };

                return base.Methods.Concat(methods);
            }
        }

        public void AssertCatalogItemWasNotFound(Slug slug) =>
            CatalogItemNotFoundMethod.ShouldBeInvokedWithTheArgument(slug);

        public void AssertBrandWasNotFound(Slug slug) =>
            BrandNotFoundMethod.ShouldBeInvokedWithTheArgument(slug);

        public void AssertScaleWasNotFound(Slug slug) =>
            ScaleNotFoundMethod.ShouldBeInvokedWithTheArgument(slug);

        public void AssertRailwayWasNotFound(IEnumerable<Slug> railways) =>
            RailwayNotFoundMethod.ShouldBeInvokedWithTheArgument(railways);
    }
}
