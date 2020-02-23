using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetBrandsList;
using TreniniDotNet.Application.InMemory.Repositories;
using TreniniDotNet.Application.InMemory.Repositories.Catalog;
using TreniniDotNet.Domain.Catalog.Brands;
using Xunit;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public sealed class GetBrandsListTests
    {
        [Fact]
        public async Task GetBrandsList_ShouldReturnEmptyResult_WhenNoBrandIsFound()
        {
            var brandsRepository = new BrandRepository(new InMemoryContext());

            GetBrandsListOutput output = null;
            var (useCase, outputPortMock) = ArrangeUseCase(brandsRepository, o => output = o);

            await useCase.Execute(new GetBrandsListInput());

            outputPortMock.Verify(outputPort => outputPort.Standard(It.IsAny<GetBrandsListOutput>()), Times.Once);
            Assert.True(output.Result.Count() == 0);
        }

        [Fact]
        public async Task GetBrandsList_ShouldReturnBrandsList()
        {
            var brandsRepository = new BrandRepository(InMemoryContext.WithCatalogSeedData());

            GetBrandsListOutput output = null;
            var (useCase, outputPortMock) = ArrangeUseCase(brandsRepository, o => output = o);

            await useCase.Execute(new GetBrandsListInput());

            outputPortMock.Verify(outputPort => outputPort.Standard(It.IsAny<GetBrandsListOutput>()), Times.Once);
            Assert.True(output.Result.Count() > 0);
        }

        private (GetBrandsList, Mock<IGetBrandsListOutputPort>) ArrangeUseCase(BrandRepository repo, Action<GetBrandsListOutput> callback)
        {
            var outputPortMock = new Mock<IGetBrandsListOutputPort>();
            outputPortMock.Setup(h => h.Standard(It.IsAny<GetBrandsListOutput>()))
                .Callback<GetBrandsListOutput>(callback);

            var brandService = new BrandService(repo);
            return (new GetBrandsList(outputPortMock.Object, brandService), outputPortMock);
        }

    }
}
