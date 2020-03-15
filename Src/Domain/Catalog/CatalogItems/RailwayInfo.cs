using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class RailwayInfo : IRailwayInfo
    {
        private readonly RailwayId railwayId;
        private readonly Slug slug;
        private readonly string name;
        private readonly string? country;

        public RailwayInfo(Guid railwayId, string slug, string name, string? country)
            : this(new RailwayId(railwayId), Slug.Of(slug), name, country)
        {
        }

        public RailwayInfo(RailwayId railwayId, Slug slug, string name, string? country)
        {
            this.railwayId = railwayId;
            this.slug = slug;
            this.name = name;
            this.country = country;
        }

        public RailwayId RailwayId => railwayId;

        public Slug Slug => slug;

        public string Name => name;

        public string? Country => country;

        public IRailwayInfo ToRailwayInfo()
        {
            return this;
        }
    }
}
