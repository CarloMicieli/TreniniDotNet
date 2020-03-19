using System.Data;
using Dapper;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace Infrastructure.Persistence.TypeHandlers
{
    public class EraTypeHandler : SqlMapper.TypeHandler<Era?>
    {
        public override Era? Parse(object value) => value?.ToString()?.ToEra();

        public override void SetValue(IDbDataParameter parameter, Era? value)
        {
            parameter.Value = value?.ToString();
        }
    }
}