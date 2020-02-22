using System;

namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    public readonly struct GuidId : IEquatable<GuidId>
    {
        private readonly Guid _id;

        public static GuidId NewId()
        {
            return new GuidId(Guid.NewGuid());
        }

        public static GuidId Empty => new GuidId(Guid.Empty);

        public GuidId(Guid id)
        {
            _id = id;
        }

        public Guid ToGuid() => _id;

        public override string ToString() => _id.ToString();

        public override int GetHashCode() => _id.GetHashCode();

        #region [ Equality ]
        public override bool Equals(object obj)
        {
            if (obj is GuidId that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public static bool operator ==(GuidId left, GuidId right)
        {
            return AreEquals(left, right);
        }

        public static bool operator !=(GuidId left, GuidId right)
        {
            return !AreEquals(left, right);
        }

        public bool Equals(GuidId other)
        {
            return AreEquals(this, other);
        }

        private static bool AreEquals(GuidId left, GuidId right)
        {
            return left._id == right._id;
        }
        #endregion
    }
}
