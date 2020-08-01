using System.Data;
using Dapper;
using TreniniDotNet.Common;
using TreniniDotNet.SharedKernel.Slugs;

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
