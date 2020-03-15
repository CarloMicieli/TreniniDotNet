using TreniniDotNet.Application.Boundaries.Catalog.GetCatalogItemBySlug;
using TreniniDotNet.Application.InMemory.OutputPorts;

namespace TreniniDotNet.Application.UnitTests.InMemory.OutputPorts.Catalog
{
    public class GetCatalogItemBySlugOutputPort : OutputPortTestHelper<GetCatalogItemBySlugOutput>, IGetCatalogItemBySlugOutputPort
    {
        public MethodInvocation<string> CatalogItemNotFoundMethod { set; get; }

        public GetCatalogItemBySlugOutputPort()
        {
            this.CatalogItemNotFoundMethod = MethodInvocation<string>.NotInvoked(nameof(CatalogItemNotFound));
        }

        public void CatalogItemNotFound(string message)
        {
            this.CatalogItemNotFoundMethod = this.CatalogItemNotFoundMethod.Invoked(message);
        }
    }
}