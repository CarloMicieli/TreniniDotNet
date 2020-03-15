using Dapper;
using System;
using System.Data;

namespace TreniniDotNet.Infrastracture.Persistence.TypeHandlers
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
