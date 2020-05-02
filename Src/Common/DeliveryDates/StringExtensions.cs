namespace TreniniDotNet.Common.DeliveryDates
{
    public static class StringExtensions
    {
        public static DeliveryDate? ToDeliveryDateOpt(this string? str)
        {
            if (str is null)
            {
                return null;
            }

            return DeliveryDate.TryParse(str, out var dd) ? dd : null;
        }
    }
}
