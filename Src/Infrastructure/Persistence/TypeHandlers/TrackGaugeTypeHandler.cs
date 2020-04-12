using Dapper;
using System.Data;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Domain.Catalog.ValueObjects;

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

            return EnumHelpers.OptionalValueFor<TrackGauge>(value.ToString());
        }

        public override void SetValue(IDbDataParameter parameter, TrackGauge? value)
        {
            parameter.Value = value?.ToString();
        }
    }
}
