using System;
using System.Data;
using Dapper;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Infrastructure.Persistence.TypeHandlers
{
    public class CatalogItemIdTypeHandler : SqlMapper.TypeHandler<CatalogItemId?>
    {
        public override CatalogItemId? Parse(object value)
        {
            if (value is null)
                return null;

            if (Guid.TryParse(value.ToString(), out var id))
            {
                return new CatalogItemId(id);
            }

            return CatalogItemId.Empty;
        }

        public override void SetValue(IDbDataParameter parameter, CatalogItemId? value)
        {
            parameter.Value = value?.ToGuid();
        }
    }
}
