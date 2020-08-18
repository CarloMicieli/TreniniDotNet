using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.Catalog.Brands.CreateBrand
{
    public sealed class CreateBrandOutputPort : OutputPortTestHelper<CreateBrandOutput>, ICreateBrandOutputPort
    {
        private MethodInvocation<Slug> BrandAlreadyExistsMethod { set; get; }

        public CreateBrandOutputPort()
        {
            BrandAlreadyExistsMethod = NewMethod<Slug>(nameof(BrandAlreadyExists));
        }

        public void BrandAlreadyExists(Slug brand)
        {
            this.BrandAlreadyExistsMethod = this.BrandAlreadyExistsMethod.Invoked(brand);
        }

        public void ShouldHaveBrandAlreadyExistsMessage(Slug expectedBrand)
        {
            this.BrandAlreadyExistsMethod.ShouldBeInvokedWithTheArgument(expectedBrand);
        }

        public override IEnumerable<IMethodInvocation> Methods
        {
            get
            {
                var methods = new List<IMethodInvocation>
                {
                    BrandAlreadyExistsMethod
                };

                return base.Methods.Concat(methods);
            }
        }
    }
}
