using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NodaMoney;
using TreniniDotNet.Common.Extensions;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public sealed class Budget
    {
        private Money Value { get; }

        public Budget(decimal amount, string currency)
        {
            if (amount.IsNegative())
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            Value = new Money(amount, NodaMoney.Currency.FromCode(currency));
        }

        public decimal Amount => Value.Amount;
        public string Currency => Value.Currency.Code;

        public override string ToString() => Value.ToString();

        public static bool TryCreate(decimal amount, string currency, [NotNullWhen(true)] out Budget? price)
        {
            var validCurrency = IsValidCurrency(currency);
            if (validCurrency && amount.IsPositive())
            {
                price = new Budget(amount, currency);
                return true;
            }

            price = default;
            return false;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Budget that)
            {
                return this == that;
            }

            return false;
        }

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(Budget left, Budget right) => left.Value.Equals(right.Value);

        public static bool operator !=(Budget left, Budget right) => !(left == right);

        public static bool IsValidCurrency(string currency)
        {
            return NodaMoney.Currency
                .GetAllCurrencies()
                .Any(it => it.Code == currency);
        }
    }
}
