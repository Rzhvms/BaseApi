using System.Data;

namespace DatabaseLib.ConnectionFactories.Interfaces;

/// <summary>
/// Фабрика соединений с базой данных.
/// </summary>
public interface IDbConnectionFactory
{
    /// <summary>
    /// Открыть новое соединение с базой данных.
    /// </summary>
    IDbConnection CreateConnection();
}