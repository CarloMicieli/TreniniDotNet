namespace TreniniDotNet.SharedKernel.Lengths
{
    class TestMultipleValues : MultipleValues<Length>
    {
        public TestMultipleValues(MeasureUnit measureUnit1, MeasureUnit measureUnit2)
            : base(measureUnit1, measureUnit2, (v, mu) => Length.Of(v, mu))
        {
        }
    }
}