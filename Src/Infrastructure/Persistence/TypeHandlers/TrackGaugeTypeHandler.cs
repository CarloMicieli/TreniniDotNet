using Dapper;
using System.Data;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Infrastructure.Persistence.TypeHandlers
{
    public class TrackGaugeTypeHandler : SqlMapper.TypeHandler<TrackGauge?>
    {
        public override TrackGauge? Parse(object value)
        {
            if (value is null)
            {
                return null;
            }

            return value.ToString().ToTrackGauge();
        }

        public override void SetValue(IDbDataParameter parameter, TrackGauge? value)
        {
            parameter.Value = value?.ToString();
        }
    }
}
