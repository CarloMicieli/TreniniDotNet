using System.Data.Common;

namespace TreniniDotNet.Infrastracture.Dapper
{
    public interface IDatabaseContext
    {
        DbConnection NewConnection();
    }
}
