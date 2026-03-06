using Dapper;
using DatabaseLib.ConnectionFactories.Interfaces;
using DatabaseLib.DapperExtensions.Repositories.Base.Interfaces;

namespace DatabaseLib.DapperExtensions.Repositories.Base;

/// <inheritdoc/>
public abstract class QueryRepository<T, TKey> : IQueryRepository<T, TKey> 
    where T : class
{
    private readonly IDbConnectionFactory _connectionFactory;
    
    public QueryRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    /// <inheritdoc/>
    public virtual async Task<T?> GetByIdAsync(TKey key)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public virtual async Task<T?> GetSingleByConditionAsync(string sql, object? param = null)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public virtual Task<IEnumerable<T>> GetListAsync(string? whereSql = null, object? param = null)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public virtual async Task GetPagedListAsync(string? whereSql, object? param, int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public virtual async Task<bool> ExistsAsync(TKey key)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public virtual async Task<int> CountAsync(string? whereSql = null, object? param = null)
    {
        throw new NotImplementedException();
    }
}