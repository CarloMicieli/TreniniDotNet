namespace TreniniDotNet.Application.Collecting.Wishlists
{
    public static class NewBudgetInput
    {
        public static BudgetInput Empty => With();

        public static BudgetInput With(decimal? value = null, string currency = null) =>
            new BudgetInput(value ?? decimal.MinValue, currency ?? "");
    }
}
