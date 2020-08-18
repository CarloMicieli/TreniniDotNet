using System.Data.Common;

namespace TreniniDotNet.Infrastructure.Dapper
{
    public interface IConnectionProvider
    {
        DbConnection Create();
    }
}
