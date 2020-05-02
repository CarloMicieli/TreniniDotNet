using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.Catalog.Brands.CreateBrand
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
