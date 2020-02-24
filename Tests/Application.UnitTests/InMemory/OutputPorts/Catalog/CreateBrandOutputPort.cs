using TreniniDotNet.Application.Boundaries.Catalog.CreateBrand;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Catalog
{
    public sealed class CreateBrandOutputPort : OutputPortTestHelper<CreateBrandOutput>, ICreateBrandOutputPort
    {
        private MethodInvocation<string> BrandAlreadyExistsMethod { set; get; }

        public CreateBrandOutputPort()
        {
            BrandAlreadyExistsMethod = MethodInvocation<string>.NotInvoked(nameof(BrandAlreadyExists));
        }

        public void BrandAlreadyExists(string message)
        {
            this.BrandAlreadyExistsMethod = this.BrandAlreadyExistsMethod.Invoked(message);
        }

        public void ShouldHaveBrandAlreadyExistsMessage(string expectedMessage)
        {
            this.BrandAlreadyExistsMethod.InvokedWithArgument(expectedMessage);
        }
    }
}
