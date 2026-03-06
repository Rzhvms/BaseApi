namespace DatabaseLib.DapperExtensions.Repositories.Base.Interfaces;

/// <summary>
/// Базовый репозиторий, содержащий основные методы.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IRepository <T, TKey> : IQueryRepository<T, TKey>, ICommandRepository<T, TKey>
    where T : class
{
    
}