using System;

namespace TreniniDotNet.SharedKernel.Lengths
{
    public static class MeasureUnits
    {
        public static string GetSymbol(MeasureUnit mu) => mu switch
        {
            MeasureUnit.Millimeters => "mm",
            MeasureUnit.Inches => "in",
            MeasureUnit.Kilometers => "km",
            MeasureUnit.Miles => "mi",
            _ => throw new ArgumentOutOfRangeException("invalid measure unit")
        };
    }
}