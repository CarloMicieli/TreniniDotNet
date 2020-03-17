using System;
using System.Data;
using Dapper;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace Infrastructure.Persistence.TypeHandlers
{
    public class RollingStockIdTypeHandler : SqlMapper.TypeHandler<RollingStockId?>
    {
        public override RollingStockId? Parse(object value)
        {
            if (value is null)
                return null;

            if (Guid.TryParse(value.ToString(), out var id))
            {
                return new RollingStockId(id);
            }

            return RollingStockId.Empty;
        }

        public override void SetValue(IDbDataParameter parameter, RollingStockId? value)
        {
            parameter.Value = value?.ToGuid();
        }
    }
}