using System.Data;
using Dapper;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace Infrastructure.Persistence.TypeHandlers
{
    public class LengthTypeHandler_OLD : SqlMapper.TypeHandler<Length?>
    {
        public override Length? Parse(object value)
        {
            if (value is null)
            {
                return null;
            }

            return Length.OfMillimeters((decimal)value);
        }

        public override void SetValue(IDbDataParameter parameter, Length? value)
        {
            parameter.Value = value?.ToMillimeters();
        }
    }

    public class LengthTypeHandler : SqlMapper.TypeHandler<TreniniDotNet.Common.Lengths.Length?>
    {
        public override TreniniDotNet.Common.Lengths.Length? Parse(object value)
        {
            if (value is null)
            {
                return null;
            }

            return TreniniDotNet.Common.Lengths.Length.OfMillimeters((decimal)value);
        }

        public override void SetValue(IDbDataParameter parameter, TreniniDotNet.Common.Lengths.Length? value)
        {
            parameter.Value = value?.Value;
        }
    }
}