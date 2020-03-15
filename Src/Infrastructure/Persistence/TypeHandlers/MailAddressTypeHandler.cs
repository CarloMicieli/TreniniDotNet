using Dapper;
using System.Data;
using System.Net.Mail;

namespace TreniniDotNet.Infrastracture.Persistence.TypeHandlers
{
    public class MailAddressTypeHandler : SqlMapper.TypeHandler<MailAddress?>
    {
        public override MailAddress? Parse(object value)
        {
            if (value is null)
            {
                return null;
            }

            return new MailAddress(value.ToString());
        }

        public override void SetValue(IDbDataParameter parameter, MailAddress? value)
        {
            parameter.Value = value?.ToString();
        }
    }
}
