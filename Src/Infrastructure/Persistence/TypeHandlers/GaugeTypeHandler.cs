using Dapper;
using System.Data;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Infrastructure.Persistence.TypeHandlers
{
    public class GaugeTypeHandler : SqlMapper.TypeHandler<Gauge?>
    {
        public override Gauge? Parse(object value)
        {
            if (value is null)
            {
                return null;
            }

            return Gauge.OfMillimiters((decimal) value);
        }

        public override void SetValue(IDbDataParameter parameter, Gauge? value)
        {
            parameter.Value = value?.ToDecimal(MeasureUnit.Millimeters);
        }
    }
}
