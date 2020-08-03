using System;
using TreniniDotNet.SharedKernel.Lengths;

namespace TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks
{
    public sealed class LengthOverBuffer
    {
        private static TwoLengths TwoLengths = new TwoLengths(MeasureUnit.Inches, MeasureUnit.Millimeters);

        private LengthOverBuffer(Length inches, Length millimeters)
        {
            Inches = inches;
            Millimeters = millimeters;
        }

        public Length Inches { get; }
        public Length Millimeters { get; }

        public static LengthOverBuffer Create(decimal? inches, decimal? mm)
        {
            var (lenIn, lenMm) = TwoLengths.Create(inches, mm);
            return new LengthOverBuffer(lenIn, lenMm);
        }

        public static LengthOverBuffer? CreateOrDefault(decimal? inches, decimal? millimeters)
        {
            if (inches.HasValue || millimeters.HasValue)
            {
                var (lenIn, lenMm) = TwoLengths.Create(inches, millimeters);
                return new LengthOverBuffer(lenIn, lenMm);
            }
            else
            {
                return null;
            }
        }

        public static LengthOverBuffer OfMillimeters(decimal mm) => Create(null, mm);

        public static LengthOverBuffer OfInches(decimal inches) => Create(inches, null);

        public void Deconstruct(out Length inches, out Length millimeters)
        {
            inches = Inches;
            millimeters = Millimeters;
        }

        public override int GetHashCode() => HashCode.Combine(Inches, Millimeters);

        public override bool Equals(object? obj)
        {
            if (obj is LengthOverBuffer that)
            {
                return this == that;
            }

            return false;
        }

        public static bool operator ==(LengthOverBuffer left, LengthOverBuffer right) =>
            left.Millimeters == right.Millimeters &&
            left.Inches == right.Inches;

        public static bool operator !=(LengthOverBuffer left, LengthOverBuffer right) => !(left == right);

        public override string ToString() =>
            $"({Inches}, {Millimeters})";
    }
}
