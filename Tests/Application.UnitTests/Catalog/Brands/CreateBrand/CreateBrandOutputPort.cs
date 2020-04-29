using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Application.Catalog.Brands.CreateBrand;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.InMemory.Catalog.Brands.OutputPorts
{
    public sealed class CreateBrandOutputPort : OutputPortTestHelper<CreateBrandOutput>, ICreateBrandOutputPort
    {
        private MethodInvocation<string> BrandAlreadyExistsMethod { set; get; }

        public CreateBrandOutputPort()
        {
            BrandAlreadyExistsMethod = NewMethod<string>(nameof(BrandAlreadyExists));
        }

        public void BrandAlreadyExists(string message)
        {
            this.BrandAlreadyExistsMethod = this.BrandAlreadyExistsMethod.Invoked(message);
        }

        public void ShouldHaveBrandAlreadyExistsMessage(string expectedMessage)
        {
            this.BrandAlreadyExistsMethod.ShouldBeInvokedWithTheArgument(expectedMessage);
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
