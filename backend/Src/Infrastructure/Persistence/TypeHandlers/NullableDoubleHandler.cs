using System;
using System.Data;
using Dapper;

namespace TreniniDotNet.Infrastructure.Persistence.TypeHandlers
{
    public class NullableDoubleHandler : SqlMapper.TypeHandler<double?>
    {
        public override void SetValue(IDbDataParameter parameter, double? value)
        {
            if (value.HasValue)
            {
                parameter.Value = value.Value;
            }
            else
            {
                parameter.Value = DBNull.Value;
            }
        }

        public override double? Parse(object? value)
        {
            if (value == null || value is DBNull) return null;

            return Convert.ToDouble(value);
        }
    }
}