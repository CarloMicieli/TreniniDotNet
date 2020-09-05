using System;

namespace TreniniDotNet.SharedKernel.Lengths
{
    public sealed class TwoLengths : MultipleValues<Length>
    {
        public TwoLengths(MeasureUnit measureUnit1, MeasureUnit measureUnit2)
            : base(measureUnit1, measureUnit2, BuildFunction)
        {
        }

        private static Func<decimal, MeasureUnit, Length> BuildFunction =>
            (value, mu) => Length.Of(value, mu);
    }
}