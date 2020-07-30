using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.Catalog.Brands.EditBrand
{
    public class EditBrandOutputPort : OutputPortTestHelper<EditBrandOutput>, IEditBrandOutputPort
    {
        private MethodInvocation<Slug> BrandNotFoundMethod { set; get; }

        public EditBrandOutputPort()
        {
            BrandNotFoundMethod = NewMethod<Slug>(nameof(BrandNotFound));
        }

        public void BrandNotFound(Slug brandSlug)
        {
            BrandNotFoundMethod = BrandNotFoundMethod.Invoked(brandSlug);
        }

        public void AssertBrandWasNotFound(Slug expectedBrandSlug)
        {
            BrandNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedBrandSlug);
        }

        public override IEnumerable<IMethodInvocation> Methods
        {
            get
            {
                var methods = new List<IMethodInvocation>
                {
                    BrandNotFoundMethod
                };

                return base.Methods.Concat(methods);
            }
        }
    }
}
