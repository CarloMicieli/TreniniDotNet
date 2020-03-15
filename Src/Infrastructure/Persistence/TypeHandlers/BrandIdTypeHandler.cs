using Dapper;
using System;
using System.Data;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Infrastracture.Persistence.TypeHandlers
{
    public class BrandIdTypeHandler : SqlMapper.TypeHandler<BrandId?>
    {
        public override BrandId? Parse(object value)
        {
            if (value is null)
                return null;

            if (Guid.TryParse(value.ToString(), out var id))
            {
                return new BrandId(id);
            }

            return BrandId.Empty;
        }

        public override void SetValue(IDbDataParameter parameter, BrandId? value)
        {
            parameter.Value = value?.ToString();
        }
    }
}
