using System;

namespace TreniniDotNet.Common.Lengths
{
    public sealed class AfterConversion
    {
        public AfterConversion(decimal value, MeasureUnit from, MeasureUnit to)
        {
            Value = value;
            FromUnit = from;
            ToUnit = to;
        }

        public decimal Value { get; }
        public MeasureUnit FromUnit { get; }
        public MeasureUnit ToUnit { get; }

        public TResult Apply<TResult>(Func<decimal, MeasureUnit, TResult> func) =>
            func(FromUnit.ConvertTo(ToUnit).Convert(Value), ToUnit);

    }
}