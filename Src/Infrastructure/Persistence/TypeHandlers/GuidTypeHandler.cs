using Dapper;
using System;
using System.Data;

namespace TreniniDotNet.Infrastracture.Persistence.TypeHandlers
{
    public class GuidTypeHandler : SqlMapper.TypeHandler<Guid?>
    {
        public override Guid? Parse(object value)
        {
            if (value is null)
                return null;

            return new Guid(value.ToString());
        }

        public override void SetValue(IDbDataParameter parameter, Guid? value)
        {
            parameter.Value = value?.ToString();
        }
    }
}
