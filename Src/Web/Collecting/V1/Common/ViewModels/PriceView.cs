using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Web.Collecting.V1.Common.ViewModels
{
    public sealed class PriceView
    {
        private readonly Price _price;

        public PriceView(Price price)
        {
            _price = price;
        }

        public decimal Amount => _price.Amount;

        public string Currency => _price.Currency;
    }
}
