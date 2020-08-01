using System.Data;
using Dapper;
using TreniniDotNet.Domain.Catalog.Scales;
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

            decimal v = (decimal)value;
            return Ratio.Of(v);
        }

        public override void SetValue(IDbDataParameter parameter, Ratio? value)
        {
            parameter.Value = value?.ToDecimal();
        }
    }
}
