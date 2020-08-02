using System.Collections.Generic;
using System.Threading.Tasks;

namespace TreniniDotNet.Common.Data
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
        
        Task<int> ExecuteAsync(string cmd, object param);

        Task<TResult> ExecuteScalarAsync<TResult>(string sql, object param);

        Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object param);

        Task<TResult?> QueryFirstOrDefaultAsync<TResult>(string sql, object param)
            where TResult : class;
    }
}
