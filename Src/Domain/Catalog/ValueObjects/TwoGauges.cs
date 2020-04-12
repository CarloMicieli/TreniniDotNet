using TreniniDotNet.Common.Lengths;

namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    public sealed class TwoGauges : MultipleValues<Gauge>
    {
        public TwoGauges()
            : base(MeasureUnit.Millimeters, MeasureUnit.Inches, (value, mu) => Gauge.Of(value, mu))
        {
        }
    }
}
