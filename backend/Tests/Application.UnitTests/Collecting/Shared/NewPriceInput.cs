namespace TreniniDotNet.Application.Collecting.Shared
{
    public static class NewPriceInput
    {
        public static PriceInput Empty => With();

        public static PriceInput With(decimal? value = null, string currency = null) =>
            new PriceInput(value ?? decimal.MinValue, currency ?? "");
    }
}
