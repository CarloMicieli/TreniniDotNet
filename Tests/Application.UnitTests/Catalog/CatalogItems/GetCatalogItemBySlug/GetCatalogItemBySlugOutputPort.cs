using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Application.Catalog.CatalogItems.GetCatalogItemBySlug;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.InMemory.Catalog.CatalogItems.OutputPorts
{
    public class GetCatalogItemBySlugOutputPort : OutputPortTestHelper<GetCatalogItemBySlugOutput>, IGetCatalogItemBySlugOutputPort
    {
        public MethodInvocation<string> CatalogItemNotFoundMethod { set; get; }

        public GetCatalogItemBySlugOutputPort()
        {
            this.CatalogItemNotFoundMethod = NewMethod<string>(nameof(CatalogItemNotFound));
        }

        public void CatalogItemNotFound(string message)
        {
            this.CatalogItemNotFoundMethod = this.CatalogItemNotFoundMethod.Invoked(message);
        }

        public override IEnumerable<IMethodInvocation> Methods
        {
            get
            {
                var methods = new List<IMethodInvocation>
                {
                    CatalogItemNotFoundMethod
                };

                return base.Methods.Concat(methods);
            }
        }
    }
}