namespace DatabaseLib.DapperExtensions.Repositories.Base.Interfaces;

/// <summary>
/// Базовый репозиторий записи данных. 
/// </summary>
/// <typeparam name="T">Модель таблицы базы данных.</typeparam>
/// <typeparam name="TKey">Тип первичного ключа.</typeparam>
public interface ICommandRepository <in T, TKey> 
    where T : class
{
    /// <summary>
    /// Добавление объекта.
    /// </summary>
    /// <param name="entity">Объект.</param>
    /// <returns>Идентификатор объекта.</returns>
    Task<TKey?> InsertAsync(T entity);

    /// <summary>
    /// Множественное добавление объектов.
    /// </summary>
    /// <param name="entities">Список объектов.</param>
    Task BulkInsertAsync(IEnumerable<T> entities);

    /// <summary>
    /// Полное обновление объекта.
    /// </summary>
    /// <param name="entity">Объект.</param>
    /// <returns>Количество обновленных записей.</returns>
    Task<int> UpdateAsync(T entity);

    /// <summary>
    /// Частичное обновление объектов.
    /// </summary>
    /// <param name="id">Идентификатор объекта.</param>
    /// <param name="changes">Список измененных полей.</param>
    /// <returns>Количество обновленных записей.</returns>
    Task<int> PatchAsync(TKey id, IDictionary<string, object> changes);

    /// <summary>
    /// Удаление объекта по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объекта.</param>
    /// <returns>Количество удаленных записей.</returns>
    Task<int> DeleteByIdAsync(TKey id);
}