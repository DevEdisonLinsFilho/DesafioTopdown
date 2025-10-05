using System.Data;

namespace ProjetoTopdown.Infrastructure.Extensions;

public interface IDapperWrapper
{
    Task<IEnumerable<T>> QueryAsync<T>(
        IDbConnection connection,
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null);
    Task<int> ExecuteAsync(string sql, object? param = null);
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null);
    Task<T> ExecuteScalarAsync<T>(string sql, object? param = null);
}
