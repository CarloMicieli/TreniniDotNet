namespace TreniniDotNet.Common.Lengths
{
    public static class MeasureUnitExtensions
    {
        public static string ToString(this MeasureUnit mu, decimal value) =>
            $"{value} {MeasureUnits.GetSymbol(mu)}";

        public static IMeasureUnitConverter ConvertTo(this MeasureUnit mu, MeasureUnit other) =>
            MeasureUnitsConverters.GetConverter(mu, other);

        public static AfterConversion As(this (decimal value, MeasureUnit mu) pair, MeasureUnit other) =>
            new AfterConversion(pair.value, pair.mu, other);
    }
}