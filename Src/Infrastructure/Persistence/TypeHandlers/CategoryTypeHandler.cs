using System.Data;
using Dapper;
using static TreniniDotNet.Common.Enums.EnumHelpers;
using TreniniDotNet.Domain.Catalog.CatalogItems;

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