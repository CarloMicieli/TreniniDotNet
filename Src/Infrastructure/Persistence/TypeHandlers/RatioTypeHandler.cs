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

            int v = (int)value;
            return Ratio.Of(v / 10.0f);
        }

        public override void SetValue(IDbDataParameter parameter, Ratio? value)
        {
            parameter.Value = value?.ToDecimal();
        }
    }
}
