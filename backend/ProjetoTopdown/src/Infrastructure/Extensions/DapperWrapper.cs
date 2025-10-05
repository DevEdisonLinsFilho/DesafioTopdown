using Dapper;
using System.Data;

namespace ProjetoTopdown.Infrastructure.Extensions;
#pragma warning disable CS8603 // Possível retorno de referência nula.

public class DapperWrapper : IDapperWrapper
{
    private readonly IDbConnection _connection;

    public DapperWrapper(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<int> ExecuteAsync(string sql, object? param = null)
    {
        return await _connection.ExecuteAsync(sql, param).ConfigureAwait(false);
    }

    public async Task<T> ExecuteScalarAsync<T>(string sql, object? param = null)
    {
        return await _connection.ExecuteScalarAsync<T>(sql, param).ConfigureAwait(false);
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null)
    {
        return await _connection.QueryAsync<T>(sql, param).ConfigureAwait(false);
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(
        IDbConnection connection,
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null)
    {
        return await connection.QueryAsync<T>(
            sql,
            param,
            transaction,
            commandTimeout,
            commandType
        ).ConfigureAwait(false);
    }
}
