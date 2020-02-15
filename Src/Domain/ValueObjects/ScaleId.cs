using System;

namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    public readonly struct ScaleId : IEquatable<ScaleId>
    {
        private readonly Guid _id;

        public static ScaleId NewId()
        {
            return new ScaleId(Guid.NewGuid());
        }

        public static ScaleId Empty => new ScaleId(Guid.Empty);

        public ScaleId(Guid id)
        {
            _id = id;
        }

        public Guid ToGuid() => _id;

        public override string ToString() => _id.ToString();

        public override int GetHashCode() => _id.GetHashCode();

        #region [ Equality ]
        public override bool Equals(object obj)
        {
            if (obj is ScaleId that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public static bool operator ==(ScaleId left, ScaleId right)
        {
            return AreEquals(left, right);
        }

        public static bool operator !=(ScaleId left, ScaleId right)
        {
            return !AreEquals(left, right);
        }

        public bool Equals(ScaleId other)
        {
            return AreEquals(this, other);
        }

        private static bool AreEquals(ScaleId left, ScaleId right)
        {
            return left._id == right._id;
        }
        #endregion
    }
}
