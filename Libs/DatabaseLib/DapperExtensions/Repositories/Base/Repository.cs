namespace DatabaseLib.DapperExtensions.Repositories.Base;

public abstract class Repository<T, TKey> where T : class
{
    public Repository()
    {

    }

    public async Task<T> GetByIdAsync(TKey id)
    {

    }
    
    public async Task GetAsync(TKey id)
    {
    }
    
    public async Task GetListAsync(TKey id)
    {
    }

    public async Task InsertAsync()
    {
        
    }

    public async Task BulkInsertAsync()
    {
        
    }
    public async Task PatchAsync()
    {
        
    }
}