using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists
{
    public sealed class BudgetInput
    {
        public BudgetInput(decimal value, string currency)
        {
            Value = value;
            Currency = currency;
        }

        public decimal Value { get; }

        public string Currency { get; }

        public Budget? ToBudgetOrDefault()
        {
            return Budget.TryCreate(Value, Currency, out var p) ? p : null;
        }
    }
}
