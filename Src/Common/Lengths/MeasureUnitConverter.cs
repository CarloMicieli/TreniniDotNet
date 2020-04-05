using System;
using System.Collections.Generic;
using System.Linq;

namespace TreniniDotNet.Common.Lengths
{
    public static class MeasureUnitsConverters
    {
        private static readonly IDictionary<(MeasureUnit from, MeasureUnit to), MeasureUnitConverter> converters = Converters
            (
                new MeasureUnitConverter(MeasureUnit.Inches, MeasureUnit.Millimeters, Inches2Millimiters),
                new MeasureUnitConverter(MeasureUnit.Millimeters, MeasureUnit.Inches, Millimiters2Inches)
            );

        private static readonly IMeasureUnitConverter sameUnitConvert = new SameUnitConverter();

        private const decimal Inches2Millimiters = 25.4M;
        private const decimal Millimiters2Inches = 0.0393701M;

        public static IMeasureUnitConverter GetConverter(MeasureUnit from, MeasureUnit to)
        {
            if (from == to)
            {
                return sameUnitConvert;
            }

            if (converters.TryGetValue((from, to), out var converter))
            {
                return converter;
            }

            return new InvalidConverter(from, to);
        }

        private static IDictionary<(MeasureUnit from, MeasureUnit to), MeasureUnitConverter> Converters(params MeasureUnitConverter[] converters) =>
            converters.ToDictionary(it => (from: it.FromUnit, to: it.ToUnit), it => it);

    }

    public interface IMeasureUnitConverter
    {
        decimal Convert(decimal value, int decimals);

        decimal Convert(decimal value) => Convert(value, 2);
    }

    internal class SameUnitConverter : IMeasureUnitConverter
    {
        public decimal Convert(decimal value, int decimals) => value;
    }

    internal class InvalidConverter : IMeasureUnitConverter
    {
        public InvalidConverter(MeasureUnit fromUnit, MeasureUnit toUnit)
        {
            this.FromUnit = fromUnit;
            this.ToUnit = toUnit;
        }

        public MeasureUnit FromUnit { get; }
        public MeasureUnit ToUnit { get; }

        public decimal Convert(decimal value, int decimals) =>
            throw new InvalidOperationException($"Unable to find a suitable converter from {FromUnit} to {ToUnit}");
    }

    internal class MeasureUnitConverter : IMeasureUnitConverter
    {
        public MeasureUnitConverter(MeasureUnit fromUnit, MeasureUnit toUnit, decimal rate)
        {
            this.FromUnit = fromUnit;
            this.ToUnit = toUnit;
            this.Rate = rate;
        }

        public MeasureUnit FromUnit { get; }
        public MeasureUnit ToUnit { get; }
        public decimal Rate { get; }

        public decimal Convert(decimal value, int decimals) =>
            decimal.Round(value * Rate, decimals);
    }
}