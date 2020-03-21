using System;

namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    public readonly struct RailwayId : IEquatable<RailwayId>
    {
        private readonly Guid _id;

        public static RailwayId NewId()
        {
            return new RailwayId(Guid.NewGuid());
        }

        public static RailwayId Empty => new RailwayId(Guid.Empty);

        public RailwayId(Guid id)
        {
            _id = id;
        }

        public Guid ToGuid() => _id;

        public override string ToString() => _id.ToString();

        public override int GetHashCode() => _id.GetHashCode();

        #region [ Equality ]
        public override bool Equals(object obj)
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
            return left._id == right._id;
        }
        #endregion
    }
}
