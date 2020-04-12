using System.Data;
using Dapper;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Infrastructure.Persistence.TypeHandlers
{
    public class EraTypeHandler : SqlMapper.TypeHandler<Era?>
    {
        public Era? EnumHandlers { get; private set; }

        public override Era? Parse(object value)
        {
            if (value is null)
            {
                return null;
            }

            return EnumHelpers.OptionalValueFor<Era>(value.ToString());
        }

        public override void SetValue(IDbDataParameter parameter, Era? value)
        {
            parameter.Value = value?.ToString();
        }
    }
}