using System;
using TreniniDotNet.Common.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace TreniniDotNet.Common.Lengths
{
    // A pair of values with a <em>MeasureUnit</em>, with the chance to automatically convert 
    // a missing value to from the other converting to the expected <em>MeasureUnit</em>.
    public abstract class MultipleValues<TResult>
    {
        protected MultipleValues(MeasureUnit measureUnit1, MeasureUnit measureUnit2,
            Func<decimal, MeasureUnit, TResult> func)
        {
            if (measureUnit1 == measureUnit2)
            {
                throw new ArgumentException(nameof(measureUnit2));
            }

            MeasureUnit1 = measureUnit1;
            MeasureUnit2 = measureUnit2;
            BuildResult = func;
        }

        public MeasureUnit MeasureUnit1 { get; }
        public MeasureUnit MeasureUnit2 { get; }
        private Func<decimal, MeasureUnit, TResult> BuildResult { get; }

        public (TResult, TResult) Create(decimal value, MeasureUnit mu)
        {
            if (mu == MeasureUnit1)
            {
                return Create(value, null);
            }

            if (mu == MeasureUnit2)
            {
                return Create(null, value);
            }

            throw new ArgumentOutOfRangeException(nameof(mu), $"Invalid measure unit [valid: {MeasureUnit1}, {MeasureUnit2}]");
        }

        public (TResult, TResult) Create(decimal? left, decimal? right)
        {
            if (left.HasValue && right.HasValue)
            {
                return (
                    BuildResult(left.Value, MeasureUnit1),
                    BuildResult(right.Value, MeasureUnit2));
            }
            else if (left.HasValue)
            {
                decimal rightValue = MeasureUnit1.ConvertTo(MeasureUnit2)
                    .Convert(left.Value);
                return (
                    BuildResult(left.Value, MeasureUnit1),
                    BuildResult(rightValue, MeasureUnit2));
            }
            else if (right.HasValue)
            {
                decimal leftValue = MeasureUnit2.ConvertTo(MeasureUnit1)
                    .Convert(right.Value);
                return (
                    BuildResult(leftValue, MeasureUnit1),
                    BuildResult(right.Value, MeasureUnit2));
            }

            throw new InvalidOperationException();
        }

        public bool TryCreate(decimal? left, decimal? right,
            [NotNullWhen(true)] out (TResult, TResult)? result)
        {
            if (!ValidateValue(left) || !ValidateValue(right))
            {
                result = null;
                return false;
            }

            if (left.HasValue && right.HasValue)
            {
                result = (
                    BuildResult(left.Value, MeasureUnit1),
                    BuildResult(right.Value, MeasureUnit2));
                return true;
            }
            else if (left.HasValue)
            {
                decimal rightValue = MeasureUnit2.ConvertTo(MeasureUnit1)
                    .Convert(left.Value);
                result = (
                    BuildResult(left.Value, MeasureUnit1),
                    BuildResult(rightValue, MeasureUnit2));
                return true;
            }
            else if (right.HasValue)
            {
                decimal leftValue = MeasureUnit1.ConvertTo(MeasureUnit2)
                    .Convert(right.Value);
                result = (
                    BuildResult(leftValue, MeasureUnit1),
                    BuildResult(right.Value, MeasureUnit2));
                return true;
            }

            result = null;
            return false;
        }

        private static bool ValidateValue(decimal? val) =>
            val.HasValue && val.Value.IsPositive();
    }
}