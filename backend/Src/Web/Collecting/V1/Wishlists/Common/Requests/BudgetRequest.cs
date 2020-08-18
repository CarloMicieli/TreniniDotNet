namespace TreniniDotNet.Web.Collecting.V1.Wishlists.Common.Requests
{
    public sealed class BudgetRequest
    {
        public decimal Value { get; set; }
        public string Currency { get; set; } = null!;
    }
}
