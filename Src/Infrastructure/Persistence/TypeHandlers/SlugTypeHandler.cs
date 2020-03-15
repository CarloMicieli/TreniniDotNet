using Dapper;
using System.Data;
using TreniniDotNet.Common;

namespace TreniniDotNet.Infrastructure.Persistence.TypeHandlers
{
    public class SlugTypeHandler : SqlMapper.TypeHandler<Slug>
    {
        public override Slug Parse(object value)
        {
            return Slug.Of(value.ToString());
        }

        public override void SetValue(IDbDataParameter parameter, Slug value)
        {
            parameter.Value = value.ToString();
        }
    }
}
