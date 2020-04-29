using NodaMoney;

namespace TreniniDotNet.Web.Collecting.V1.Common.ViewModels
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
