using System.Data;
using Dapper;
using TreniniDotNet.Common;
using TreniniDotNet.SharedKernel.Countries;

namespace TreniniDotNet.Infrastructure.Persistence.TypeHandlers
{
    public class CountryTypeHandler : SqlMapper.TypeHandler<Country>
    {
        public override Country Parse(object value)
        {
            return Country.Of(value.ToString());
        }

        public override void SetValue(IDbDataParameter parameter, Country value)
        {
            parameter.Value = value.Code;
        }
    }
}
