namespace DatabaseLib.DapperExtensions.Repositories.Base.Interfaces;

/// <summary>
/// Базовый репозиторий получения данных. 
/// </summary>
/// <typeparam name="T">Модель таблицы базы данных.</typeparam>
/// <typeparam name="TKey">Тип первичного ключа.</typeparam>
public interface IQueryRepository<T, in TKey> 
    where T : class
{
    /// <summary>
    /// Получение объекта по первичному ключу.
    /// </summary>
    /// <param name="key">Идентификатор объекта.</param>
    /// <returns>Один объект по ключу.</returns>
    Task<T?> GetByIdAsync(TKey key);
    
    /// <summary>
    /// Получение одного объекта по условию.
    /// </summary>
    /// <param name="sql">SQL-запрос, который необходимо выполнить.</param>
    /// <param name="param">Параметры для выполнения запроса.</param>
    /// <returns>Один объект.</returns>
    Task<T?> GetSingleByConditionAsync(string sql, object? param = null);
    
    /// <summary>
    /// Получение списка объектов.
    /// </summary>
    /// <param name="whereSql">Where-условие.</param>
    /// <param name="param">Параметры для выполнения запроса.</param>
    /// <returns>Список объектов</returns>
    Task<IEnumerable<T>> GetListAsync(string? whereSql = null, object? param = null);
    
    /// <summary>
    /// Получение списка объектов с пагинацией.
    /// </summary>
    /// <param name="whereSql">Where-условие.</param>
    /// <param name="param">Параметры для выполнения запроса.</param>
    /// <param name="page">Номер страницы.</param>
    /// <param name="pageSize">Количество записей на странице.</param>
    /// <returns></returns>
    Task GetPagedListAsync(string? whereSql, object? param, int page, int pageSize);
    
    /// <summary>
    /// Проверка существования объекта по идентификатору.
    /// </summary>
    /// <param name="key">Идентификатор объекта.</param>
    /// <returns>Существует ли объект.</returns>
    Task<bool> ExistsAsync(TKey key);
    
    /// <summary>
    /// Получение количества объектов по условию.
    /// </summary>
    /// <param name="whereSql">Where-условие.</param>
    /// <param name="param">Параметры для выполнения запроса.</param>
    /// <returns>Количество записей.</returns>
    Task<int> CountAsync(string? whereSql = null, object? param = null);
}