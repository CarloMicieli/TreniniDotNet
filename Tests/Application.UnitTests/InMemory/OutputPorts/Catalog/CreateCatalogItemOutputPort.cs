using System.Collections.Generic;
using TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem;
using TreniniDotNet.Application.InMemory.OutputPorts;

namespace TreniniDotNet.Application.UnitTests.InMemory.OutputPorts.Catalog
{
    public class CreateCatalogItemOutputPort : OutputPortTestHelper<CreateCatalogItemOutput>, ICreateCatalogItemOutputPort
    {
        public MethodInvocation<string> BrandNameNotFoundMethod { get; private set; }
        public MethodInvocation<string> CatalogItemAlreadyExistsMethod { get; private set; }
        public MethodInvocation<string> ScaleNotFoundMethod { get; private set; }
        public MethodInvocation<string, IEnumerable<string>> RailwayNotFoundMethod { get; private set; }

        public CreateCatalogItemOutputPort()
        {
            BrandNameNotFoundMethod = MethodInvocation<string>.NotInvoked(nameof(BrandNameNotFound));
            CatalogItemAlreadyExistsMethod = MethodInvocation<string>.NotInvoked(nameof(CatalogItemAlreadyExists));
            ScaleNotFoundMethod = MethodInvocation<string>.NotInvoked(nameof(ScaleNotFound));
            RailwayNotFoundMethod = MethodInvocation<string, IEnumerable<string>>.NotInvoked(nameof(RailwayNotFound));
        }

        public void CatalogItemAlreadyExists(string message)
        {
            this.CatalogItemAlreadyExistsMethod = this.CatalogItemAlreadyExistsMethod.Invoked(message);
        }

        public void BrandNameNotFound(string message)
        {
            this.BrandNameNotFoundMethod = this.BrandNameNotFoundMethod.Invoked(message);
        }

        public void ScaleNotFound(string message)
        {
            this.ScaleNotFoundMethod = this.ScaleNotFoundMethod.Invoked(message);
        }

        public void RailwayNotFound(string message, IEnumerable<string> railwayNames)
        {
            this.RailwayNotFoundMethod = this.RailwayNotFoundMethod.Invoked(message, railwayNames);
        }
    }
}