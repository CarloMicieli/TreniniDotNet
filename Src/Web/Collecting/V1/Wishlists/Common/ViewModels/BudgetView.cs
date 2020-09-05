using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.Common.ViewModels
{
    public sealed class BudgetView
    {
        private readonly Budget _budget;

        public BudgetView(Budget budget)
        {
            _budget = budget;
        }

        public decimal Amount => _budget.Amount;

        public string Currency => _budget.Currency;
    }
}