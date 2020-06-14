using System;
using System.Data;
using Dapper;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Infrastructure.Persistence.TypeHandlers
{
    public class RailwayIdTypeHandler : SqlMapper.TypeHandler<RailwayId?>
    {
        public override RailwayId? Parse(object value)
        {
            if (value is null)
                return null;

            if (Guid.TryParse(value.ToString(), out var id))
            {
                return new RailwayId(id);
            }

            return RailwayId.Empty;
        }

        public override void SetValue(IDbDataParameter parameter, RailwayId? value)
        {
            parameter.Value = value?.ToGuid();
        }
    }
}
