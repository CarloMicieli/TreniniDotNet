using System;
using System.Data;
using Dapper;

namespace TreniniDotNet.Infrastructure.Persistence.TypeHandlers
{
    public class UriTypeHandler : SqlMapper.TypeHandler<Uri?>
    {
        public override Uri? Parse(object value)
        {
            if (value is null)
            {
                return null;
            }

            return new Uri(value.ToString());
        }

        public override void SetValue(IDbDataParameter parameter, Uri? value)
        {
            parameter.Value = value?.ToString();
        }
    }
}
