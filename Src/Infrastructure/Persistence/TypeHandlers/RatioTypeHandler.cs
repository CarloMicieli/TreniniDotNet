using Dapper;
using System.Data;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Infrastructure.Persistence.TypeHandlers
{
    public class RatioTypeHandler : SqlMapper.TypeHandler<Ratio?>
    {
        public override Ratio? Parse(object value)
        {
            if (value is null)
            {
                return null;
            }

            return Ratio.Of((decimal) value);
        }

        public override void SetValue(IDbDataParameter parameter, Ratio? value)
        {
            parameter.Value = value?.ToDecimal();
        }
    }
}
