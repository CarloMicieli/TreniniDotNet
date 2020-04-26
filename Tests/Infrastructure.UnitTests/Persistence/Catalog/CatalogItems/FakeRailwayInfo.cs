using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems
{
    public class FakeRailwayInfo : IRailwayInfo
    {
        private readonly RailwayId railwayId = new RailwayId(Guid.NewGuid());

        public RailwayId RailwayId => railwayId;

        public Slug Slug => Slug.Of("FS");

        public string Name => "FS";

        public Country Country => Country.Of("IT");
    }
}
