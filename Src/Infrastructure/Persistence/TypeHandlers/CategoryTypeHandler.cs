using System.Data;
using Dapper;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace Infrastructure.Persistence.TypeHandlers
{
    public class CategoryTypeHandler : SqlMapper.TypeHandler<Category?>
    {
        public override Category? Parse(object value)
        {
            if (value is null)
            {
                return null;
            }

            return value.ToString().ToCategory();
        }

        public override void SetValue(IDbDataParameter parameter, Category? value)
        {
            parameter.Value = value?.ToString();
        }
    }
}