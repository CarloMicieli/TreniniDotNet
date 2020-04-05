using LanguageExt;
using static LanguageExt.Prelude;
using System;
using TreniniDotNet.Common.Extensions;

namespace TreniniDotNet.Common.Lengths
{
    // A pair of lengths, with the chance to automatically convert the second value
    // from the first if missing
    public class MultipleValues<TResult>
    {
        public MultipleValues(MeasureUnit measureUnit1, MeasureUnit measureUnit2,
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
                decimal rightValue = MeasureUnit2.ConvertTo(MeasureUnit1)
                    .Convert(left.Value);
                return (
                    BuildResult(left.Value, MeasureUnit1),
                    BuildResult(rightValue, MeasureUnit2));
            }
            else if (right.HasValue)
            {
                decimal leftValue = MeasureUnit1.ConvertTo(MeasureUnit2)
                    .Convert(right.Value);
                return (
                    BuildResult(leftValue, MeasureUnit1),
                    BuildResult(right.Value, MeasureUnit2));
            }

            throw new InvalidOperationException();
        }

        public Validation<Error, (TResult, TResult)> TryCreate(decimal? left, decimal? right)
        {
            if (left.HasValue && right.HasValue)
            {
                var leftV = NonNegative(left.Value, $"{left.Value} {MeasureUnit1} is not a valid value (negative)");
                var rightV = NonNegative(right.Value, $"{right.Value} {MeasureUnit2} is not a valid value (negative)");
                return (leftV, rightV).Apply((lhs, rhs) => this.Create(lhs, rhs));
            }
            else if (left.HasValue)
            {
                var leftV = NonNegative(left.Value, $"{left.Value} {MeasureUnit1} is not a valid value (negative)");
                return leftV.Select(lhs => this.Create(lhs, null));
            }
            else if (right.HasValue)
            {
                var rightV = NonNegative(right.Value, $"{right.Value} {MeasureUnit2} is not a valid value (negative)");
                return rightV.Select(rhs => this.Create(null, rhs));
            }

            return Fail<Error, (TResult, TResult)>(Error.New("Both left and right values are null"));
        }

        private static Validation<Error, decimal> NonNegative(decimal value, string because) =>
            value.IsPositive() ? Success<Error, decimal>(value) : Fail<Error, decimal>(because);
    }
}