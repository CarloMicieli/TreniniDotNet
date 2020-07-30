using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.InMemory.Domain;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public sealed class RailwaysServiceTests
    {
        private Guid ExpectedId { get; }

        private RailwaysService Service { get; }
        private Mock<IRailwaysRepository> RepositoryMock { get; }

        public RailwaysServiceTests()
        {
            ExpectedId = Guid.NewGuid();
            RepositoryMock = new Mock<IRailwaysRepository>();

            var factory = Factories<RailwaysFactory>
                .New((clock, guidSource) => new RailwaysFactory(clock, guidSource))
                .Id(ExpectedId)
                .Build();

            Service = new RailwaysService(RepositoryMock.Object, factory);
        }

        [Fact]
        public async Task RailwaysService_RailwayAlreadyExists_ShouldCheckIfRailwayExists()
        {
            var fs = CatalogSeedData.Railways.NewFs();
            RepositoryMock.Setup(x => x.ExistsAsync(fs.Slug))
                .ReturnsAsync(true);

            bool exists1 = await Service.RailwayAlreadyExists(fs.Slug);
            bool exists2 = await Service.RailwayAlreadyExists(Slug.Of("not found"));

            exists1.Should().BeTrue();
            exists2.Should().BeFalse();
        }

        [Fact]
        public async Task RailwaysService_CreateRailway_ShouldCreateNewRailways()
        {
            var fs = CatalogSeedData.Railways.NewFs();

            RepositoryMock.Setup(x => x.AddAsync(It.IsAny<Railway>()))
                .ReturnsAsync(new RailwayId(ExpectedId));

            var id = await Service.CreateRailway(
                fs.Name,
                fs.CompanyName,
                fs.Country,
                fs.PeriodOfActivity,
                fs.TotalLength,
                fs.TrackGauge,
                fs.WebsiteUrl,
                fs.Headquarters);

            id.Should().Be(new RailwayId(ExpectedId));
        }

        [Fact]
        public async Task RailwaysService_GetBySlug_ShouldFindRailways()
        {
            var fs = CatalogSeedData.Railways.NewFs();

            RepositoryMock.Setup(x => x.GetBySlugAsync(fs.Slug))
                .ReturnsAsync(fs);

            var railway1 = await Service.GetBySlugAsync(fs.Slug);
            var railway2 = await Service.GetBySlugAsync(Slug.Of("not found"));

            railway1.Should().NotBeNull();
            railway1.Name.Should().Be(fs.Name);
            railway1.Slug.Should().Be(fs.Slug);

            railway2.Should().BeNull();
        }

        [Fact]
        public async Task RailwaysService_UpdateRailway_ShouldUpdateRailways()
        {
            var fs = CatalogSeedData.Railways.NewFs();

            RepositoryMock.Setup(x => x.UpdateAsync(fs))
                .Returns(Task.CompletedTask);

            await Service.UpdateRailway(fs);
        }

        [Fact]
        public async Task RailwaysService_FindAllRailways_ShouldReturnRailways()
        {
            var page = new Page();

            RepositoryMock.Setup(x => x.GetAllAsync(page))
                .ReturnsAsync(new PaginatedResult<Railway>(page, new List<Railway>()));

            var results = await Service.FindAllRailways(page);

            results.Should().NotBeNull();
            results.Results.Should().HaveCount(0);
        }
    }
}
