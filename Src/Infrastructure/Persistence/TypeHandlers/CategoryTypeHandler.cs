using System.Data;
using Dapper;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using static TreniniDotNet.Common.Enums.EnumHelpers;

namespace Infrastructure.Infrastructure.TypeHandlers
{
    public class CategoryTypeHandler : SqlMapper.TypeHandler<Category?>
    {
        public override Category? Parse(object value)
        {
            if (value is null)
            {
                return null;
            }

            return OptionalValueFor<Category>(value.ToString());
        }

        public override void SetValue(IDbDataParameter parameter, Category? value)
        {
            parameter.Value = value?.ToString();
        }
    }
}