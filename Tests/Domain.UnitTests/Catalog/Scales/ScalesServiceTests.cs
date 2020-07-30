using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.InMemory.Domain;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public class ScalesServiceTests
    {
        private Guid ExpectedId { get; }

        private ScalesService Service { get; }
        private Mock<IScalesRepository> RepositoryMock { get; }

        public ScalesServiceTests()
        {
            ExpectedId = Guid.NewGuid();
            RepositoryMock = new Mock<IScalesRepository>();

            var factory = Factories<ScalesFactory>
                .New((clock, guidSource) => new ScalesFactory(clock, guidSource))
                .Id(ExpectedId)
                .Build();

            Service = new ScalesService(RepositoryMock.Object, factory);
        }

        [Fact]
        public async Task ScalesService_ScaleAlreadyExists_ShouldCheckScaleExists()
        {
            RepositoryMock.Setup(x => x.ExistsAsync(Slug.Of("h0")))
                .ReturnsAsync(true);

            var exists1 = await Service.ScaleAlreadyExists(Slug.Of("h0"));
            var exists2 = await Service.ScaleAlreadyExists(Slug.Of("not found"));

            exists1.Should().BeTrue();
            exists2.Should().BeFalse();
        }

        [Fact]
        public async Task ScalesService_CreateScale_ShouldCreateNewScales()
        {
            var scale = CatalogSeedData.Scales.ScaleH0();

            RepositoryMock.Setup(x => x.AddAsync(It.IsAny<Scale>()))
                .ReturnsAsync(scale.Id);

            var id = await Service.CreateScale(
                scale.Name,
                scale.Ratio,
                scale.Gauge,
                scale.Description,
                scale.Standards.ToHashSet(),
                scale.Weight);

            id.Should().Be(scale.Id);
        }

        [Fact]
        public async Task ScalesService_GetBySlugAsync_ShouldFindScalesBySlug()
        {
            var scaleH0 = CatalogSeedData.Scales.ScaleH0();

            RepositoryMock.Setup(x => x.GetBySlugAsync(scaleH0.Slug))
                .ReturnsAsync(scaleH0);

            var scale1 = await Service.GetBySlugAsync(scaleH0.Slug);
            var scale2 = await Service.GetBySlugAsync(Slug.Of("not found"));

            scale1.Should().NotBeNull();
            scale1.Id.Should().Be(scaleH0.Id);

            scale2.Should().BeNull();
        }

        [Fact]
        public async Task ScalesService_UpdateScaleAsync_ShouldUpdateScales()
        {
            var scaleH0 = CatalogSeedData.Scales.ScaleH0();

            RepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Scale>()))
                .Returns(Task.CompletedTask);

            await Service.UpdateScaleAsync(scaleH0);
        }

        [Fact]
        public async Task ScalesService_FindAllScales_ShouldReturnScales()
        {
            var page = new Page();
            RepositoryMock.Setup(x => x.GetAllAsync(page))
                .ReturnsAsync(new PaginatedResult<Scale>(page, new List<Scale>()));

            var results = await Service.FindAllScales(page);

            results.Should().NotBeNull();
            results.Results.Should().HaveCount(0);
        }
    }
}
