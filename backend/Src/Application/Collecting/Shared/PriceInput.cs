using System;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Application.Collecting.Shared
{
    public sealed class PriceInput
    {
        public PriceInput(decimal value, string currency)
        {
            Value = value;
            Currency = currency;
        }

        public decimal Value { get; }

        public string Currency { get; }

        public Price ToPrice() => ToPriceOrDefault() ?? throw new InvalidOperationException();

        public Price? ToPriceOrDefault()
        {
            return Price.TryCreate(Value, Currency, out var p) ? p : null;
        }
    }
}
