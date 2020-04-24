using NodaMoney;

namespace TreniniDotNet.Web.ViewModels.V1.Collection
{
    public sealed class MoneyView
    {
        private readonly Money _money;

        public MoneyView(Money money)
        {
            _money = money;
        }

        public decimal Amount => _money.Amount;

        public string Currency => _money.Currency.Code;
    }
}
