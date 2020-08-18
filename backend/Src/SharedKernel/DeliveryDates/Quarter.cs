namespace TreniniDotNet.SharedKernel.DeliveryDates
{
    public enum Quarter
    {
        Q1,
        Q2,
        Q3,
        Q4
    }

    internal static class Quarters
    {
        internal static int QuarterToInt(Quarter? qtr) =>
            qtr switch
            {
                Quarter.Q1 => 1,
                Quarter.Q2 => 2,
                Quarter.Q3 => 3,
                Quarter.Q4 => 4,
                _ => int.MaxValue
            };
    }
}