using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public sealed class RailwayInfo : IRailwayInfo, IEquatable<RailwayInfo>
    {
        public RailwayInfo(Guid railwayId, string slug, string name)
            : this(new RailwayId(railwayId), Slug.Of(slug), name)
        {
        }

        public RailwayInfo(RailwayId railwayId, Slug slug, string name)
        {
            Id = railwayId;
            Slug = slug;
            Name = name;
        }

        public RailwayId Id { get; }

        public Slug Slug { get; }

        public string Name { get; }

        public override int GetHashCode() => Id.GetHashCode();

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
            left.Id.Equals(right.Id);
    }
}
