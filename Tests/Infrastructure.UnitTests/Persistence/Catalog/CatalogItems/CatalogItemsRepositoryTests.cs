using System;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Infrastracture.Dapper;
using TreniniDotNet.Infrastructure.Database.Testing;
using NodaTime;
using Xunit;
using FluentAssertions;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Common;
using System.Collections.Generic;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems
{
    public class CatalogItemsRepositoryTests : RepositoryUnitTests<ICatalogItemRepository>
    {
        public CatalogItemsRepositoryTests(SqliteDatabaseFixture fixture) 
            : base(fixture, CreateRepository)
        {
        }

        private static ICatalogItemRepository CreateRepository(IDatabaseContext databaseContext, IClock clock)
            => new CatalogItemRepository(
                databaseContext,
                new CatalogItemsFactory(clock, FakeGuidSource.NewSource(Guid.NewGuid())),
                new RollingStocksFactory(clock, FakeGuidSource.NewSource(Guid.NewGuid())));

    }

    public class TestCatalogItem : ICatalogItem
    {
        public CatalogItemId CatalogItemId => throw new NotImplementedException();

        public IBrandInfo Brand => throw new NotImplementedException();

        public Slug Slug => throw new NotImplementedException();

        public ItemNumber ItemNumber => throw new NotImplementedException();

        public IReadOnlyList<IRollingStock> RollingStocks => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();

        public string PrototypeDescription => throw new NotImplementedException();

        public string ModelDescription => throw new NotImplementedException();

        public IScaleInfo Scale => throw new NotImplementedException();

        public PowerMethod PowerMethod => throw new NotImplementedException();
    }

    public class TestRollingStock : IRollingStock
    {
        private RollingStockId _id;

        public TestRollingStock(Guid id)
        {
            _id = new RollingStockId(id);
        }

        public RollingStockId RollingStockId => _id;

        public IRailwayInfo Railway => throw new NotImplementedException();

        public Category Category => throw new NotImplementedException();

        public Era Era => throw new NotImplementedException();

        public Length Length => throw new NotImplementedException();

        public string ClassName => throw new NotImplementedException();

        public string RoadNumber => throw new NotImplementedException();

        public string TypeName => throw new NotImplementedException();

        public DccInterface DccInterface => throw new NotImplementedException();

        public Control Control => throw new NotImplementedException();
    }
}
