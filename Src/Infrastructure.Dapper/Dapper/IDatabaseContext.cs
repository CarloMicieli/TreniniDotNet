using System.Data.Common;

namespace TreniniDotNet.Infrastructure.Dapper
{
    public interface IDatabaseContext
    {
        DbConnection NewConnection();
    }
}
