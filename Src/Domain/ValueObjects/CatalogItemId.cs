using System;

namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    public readonly struct CatalogItemId : IEquatable<CatalogItemId>
    {
        private readonly Guid _id;

        public static CatalogItemId NewId()
        {
            return new CatalogItemId(Guid.NewGuid());
        }

        public static CatalogItemId Empty => new CatalogItemId(Guid.Empty);

        public CatalogItemId(Guid id)
        {
            _id = id;
        }

        public Guid ToGuid() => _id;

        public override string ToString() => _id.ToString();

        public override int GetHashCode() => _id.GetHashCode();

        #region [ Equality ]
        public override bool Equals(object obj)
        {
            if (obj is CatalogItemId that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public static bool operator ==(CatalogItemId left, CatalogItemId right)
        {
            return AreEquals(left, right);
        }

        public static bool operator !=(CatalogItemId left, CatalogItemId right)
        {
            return !AreEquals(left, right);
        }

        public bool Equals(CatalogItemId other)
        {
            return AreEquals(this, other);
        }

        private static bool AreEquals(CatalogItemId left, CatalogItemId right)
        {
            return left._id == right._id;
        }
        #endregion
    }
}
