using System;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public readonly struct RailwayId : IEquatable<RailwayId>, IComparable<RailwayId>
    {
        private Guid Id { get; }

        public static RailwayId NewId()
        {
            return new RailwayId(Guid.NewGuid());
        }

        public static RailwayId Empty => new RailwayId(Guid.Empty);

        public RailwayId(Guid id)
        {
            Id = id;
        }

        public static implicit operator Guid(RailwayId railwayId) => railwayId.Id;
        public static explicit operator RailwayId(Guid id) => new RailwayId(id);

        public override string ToString() => Id.ToString();

        public override int GetHashCode() => Id.GetHashCode();

        #region [ Equality ]
        public override bool Equals(object? obj)
        {
            if (obj is RailwayId that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public static bool operator ==(RailwayId left, RailwayId right)
        {
            return AreEquals(left, right);
        }

        public static bool operator !=(RailwayId left, RailwayId right)
        {
            return !AreEquals(left, right);
        }

        public bool Equals(RailwayId other)
        {
            return AreEquals(this, other);
        }

        private static bool AreEquals(RailwayId left, RailwayId right)
        {
            return left.Id == right.Id;
        }
        #endregion

        public int CompareTo(RailwayId other)
        {
            return Id.CompareTo(other.Id);
        }
    }
}