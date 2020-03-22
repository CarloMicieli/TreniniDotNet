using System;

namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    /// <summary>
    /// It represent a catalog item number. 
    /// 
    /// It is immutable, and only non empty values are valid to construct new ItemNumber values.
    /// </summary>
    public readonly struct ItemNumber : IEquatable<ItemNumber>
    {
        private readonly string _value;

        public ItemNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidItemNumberException(ExceptionMessages.InvalidItemNumber);
            }

            _value = value;
        }

        public string Value => _value;

        public static bool TryCreate(string v, out ItemNumber itemNumber)
        {
            if (string.IsNullOrWhiteSpace(v))
            {
                itemNumber = default;
                return false;
            }
            else
            {
                itemNumber = new ItemNumber(v);
                return true;
            }
        }

        #region [ Standard method overrides ]
        public override string ToString()
        {
            return _value;
        }

        public override int GetHashCode() => _value.GetHashCode(StringComparison.InvariantCultureIgnoreCase);

        public override bool Equals(object obj)
        {
            ItemNumber? that = obj as ItemNumber?;
            if (that != null)
            {
                return EqualOperator(this, that.Value);
            }

            return false;
        }

        public static bool operator ==(ItemNumber left, ItemNumber right)
        {
            return EqualOperator(left, right);
        }

        public static bool operator !=(ItemNumber left, ItemNumber right)
        {
            return !EqualOperator(left, right);
        }

        public bool Equals(ItemNumber other)
        {
            return EqualOperator(this, other);
        }

        private static bool EqualOperator(ItemNumber @this, ItemNumber that)
        {
            return @this.Value == that.Value;
        }
        #endregion
    }
}
