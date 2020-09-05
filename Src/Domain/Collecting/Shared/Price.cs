using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NodaMoney;
using TreniniDotNet.Common.Extensions;

namespace TreniniDotNet.Domain.Collecting.Shared
{
    public sealed class Price
    {
        public const string Eur = "EUR";
        public const string Usd = "USD";
        public const string Gbp = "GBP";

        private Money Value { get; }

        public Price(decimal amount, string currency)
        {
            if (amount.IsNegative())
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            Value = new Money(amount, NodaMoney.Currency.FromCode(currency));
        }

        public static bool TryCreate(decimal amount, string currency, [NotNullWhen(true)] out Price? price)
        {
            var validCurrency = IsValidCurrency(currency);
            if (validCurrency && amount.IsPositive())
            {
                price = new Price(amount, currency);
                return true;
            }

            price = default;
            return false;
        }

        public static Price Euro(decimal amount) =>
            new Price(amount, Eur);

        public static Price Pounds(decimal amount) =>
            new Price(amount, Gbp);

        public static Price Dollars(decimal amount) =>
            new Price(amount, Usd);

        public decimal Amount => Value.Amount;
        public string Currency => Value.Currency.Code;

        public override bool Equals(object? obj)
        {
            if (obj is Price that)
            {
                return this == that;
            }

            return false;
        }

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(Price left, Price right) => left.Value.Equals(right.Value);

        public static bool operator !=(Price left, Price right) => !(left == right);

        public Price Add(Price other)
        {
            return new Price(this.Amount + other.Amount, this.Currency);
        }

        public static bool IsValidCurrency(string currency)
        {
            return NodaMoney.Currency
                .GetAllCurrencies()
                .Any(it => it.Code == currency);
        }

        public override string ToString() => Value.ToString();
    }

    public static class EnumerableExtensions
    {
        public static Price Sum(this IEnumerable<Price> en)
        {
            var zero = en.FirstOrDefault();
            if (zero is null)
            {
                return Price.Euro(0M);
            }
            else
            {
                return en.Skip(1).Aggregate(zero, (acc, l) => acc.Add(l));
            }
        }
    }
}
