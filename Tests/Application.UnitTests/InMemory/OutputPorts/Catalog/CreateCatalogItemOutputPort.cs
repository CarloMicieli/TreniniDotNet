using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem;
using TreniniDotNet.Application.InMemory.OutputPorts;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using Xunit;

namespace TreniniDotNet.Application.UnitTests.InMemory.OutputPorts.Catalog
{
    public class CreateCatalogItemOutputPort : OutputPortTestHelper<CreateCatalogItemOutput>, ICreateCatalogItemOutputPort
    {
        public MethodInvocation<Slug> BrandNotFoundMethod { get; set; }
        public MethodInvocation<IBrandInfo, ItemNumber> CatalogItemAlreadyExistsMethod { get; set; }
        public MethodInvocation<Slug> ScaleNotFoundMethod { get; set; }
        public MethodInvocation<IEnumerable<Slug>> RailwayNotFoundMethod { get; set; }

        public CreateCatalogItemOutputPort()
        {
            BrandNotFoundMethod = NewMethod<Slug>(nameof(BrandNotFound));
            CatalogItemAlreadyExistsMethod = NewMethod<IBrandInfo, ItemNumber>(nameof(CatalogItemAlreadyExists));
            ScaleNotFoundMethod = NewMethod<Slug>(nameof(ScaleNotFound));
            RailwayNotFoundMethod = NewMethod<IEnumerable<Slug>>(nameof(RailwayNotFound));
        }

        public void BrandNotFound(Slug brand)
        {
            BrandNotFoundMethod = BrandNotFoundMethod.Invoked(brand);
        }

        public void CatalogItemAlreadyExists(IBrandInfo brand, ItemNumber itemNumber)
        {
            CatalogItemAlreadyExistsMethod = CatalogItemAlreadyExistsMethod.Invoked(brand, itemNumber);
        }

        public void ScaleNotFound(Slug scale)
        {
            ScaleNotFoundMethod = ScaleNotFoundMethod.Invoked(scale);
        }

        public void RailwayNotFound(IEnumerable<Slug> railways)
        {
            RailwayNotFoundMethod = RailwayNotFoundMethod.Invoked(railways);
        }

        public void AssertBrandWasNotFound(Slug expectedBrand)
        {
            BrandNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedBrand);
        }

        public void AssertCatalogItemAlreadyExists(IBrandInfo expectedBrand, ItemNumber expectedItemNumber)
        {
            CatalogItemAlreadyExistsMethod.ShouldBeInvokedWithTheArguments(expectedBrand, expectedItemNumber);
        }

        public void AssertScaleWasNotFound(Slug expectedScale)
        {
            ScaleNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedScale);
        }

        public void AssertRailwayWasNotFound(IEnumerable<Slug> expectedRailways)
        {
            RailwayNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedRailways);
        }

        public override IEnumerable<IMethodInvocation> Methods
        {
            get
            {
                var methods = new List<IMethodInvocation>
                {
                    BrandNotFoundMethod,
                    CatalogItemAlreadyExistsMethod,
                    ScaleNotFoundMethod,
                    RailwayNotFoundMethod
                };

                return base.Methods.Concat(methods);
            }
        }
    }
}