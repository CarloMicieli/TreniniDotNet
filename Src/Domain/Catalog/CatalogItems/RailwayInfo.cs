﻿using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class RailwayInfo : IRailwayInfo, IEquatable<RailwayInfo>
    {
        public RailwayInfo(Guid railwayId, string slug, string name, string? country)
            : this(new RailwayId(railwayId), Slug.Of(slug), name, country)
        {
        }

        public RailwayInfo(RailwayId railwayId, Slug slug, string name, string? country)
        {
            RailwayId = railwayId;
            Slug = slug;
            Name = name;
            Country = country;
        }

        public RailwayId RailwayId { get; }

        public Slug Slug { get; }

        public string Name { get; }

        public string? Country { get; }

        public override int GetHashCode() => RailwayId.GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj is RailwayInfo that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public bool Equals(RailwayInfo other) =>
            AreEquals(this, other);

        private static bool AreEquals(RailwayInfo left, RailwayInfo right) =>
            left.RailwayId.Equals(right.RailwayId);

        public IRailwayInfo ToRailwayInfo()
        {
            return this;
        }
    }
}
