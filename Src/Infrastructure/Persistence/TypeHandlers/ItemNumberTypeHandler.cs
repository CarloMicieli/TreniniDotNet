using System.Data;
using Dapper;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Infrastructure.Persistence.TypeHandlers
{
    public class ItemNumberTypeHandler : SqlMapper.TypeHandler<ItemNumber>
    {
        public override ItemNumber Parse(object value)
        {
            return new ItemNumber(value.ToString());
        }

        public override void SetValue(IDbDataParameter parameter, ItemNumber value)
        {
            parameter.Value = value.ToString();
        }
    }
}
