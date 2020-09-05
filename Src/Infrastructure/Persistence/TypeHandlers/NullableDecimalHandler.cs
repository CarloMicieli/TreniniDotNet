using System;
using System.Data;
using Dapper;

namespace TreniniDotNet.Infrastructure.Persistence.TypeHandlers
{
    public class NullableDecimalHandler : SqlMapper.TypeHandler<decimal?>
    {
        public override void SetValue(IDbDataParameter parameter, decimal? value)
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

        public override decimal? Parse(object? value)
        {
            if (value == null || value is DBNull) return null;

            return Convert.ToDecimal(value);
        }
    }
}