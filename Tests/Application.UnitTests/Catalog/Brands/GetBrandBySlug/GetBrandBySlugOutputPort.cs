using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Application.Catalog.Brands.GetBrandBySlug;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.InMemory.Catalog.Brands.OutputPorts
{
    public sealed class GetBrandBySlugOutputPort : OutputPortTestHelper<GetBrandBySlugOutput>, IGetBrandBySlugOutputPort
    {
        private MethodInvocation<string> BrandNotFoundMethod { set; get; }

        public GetBrandBySlugOutputPort()
        {
            this.BrandNotFoundMethod = MethodInvocation<string>.NotInvoked(nameof(BrandNotFound));
        }

        public void BrandNotFound(string message)
        {
            this.BrandNotFoundMethod = this.BrandNotFoundMethod.Invoked(message);
        }

        public void ShouldHaveBrandNotFoundMessage(string expectedMessage)
        {
            this.BrandNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedMessage);
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
