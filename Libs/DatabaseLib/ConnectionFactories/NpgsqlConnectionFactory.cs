using System.Data;
using DatabaseLib.ConnectionFactories.Interfaces;
using Npgsql;

namespace DatabaseLib.ConnectionFactories;

/// <inheritdoc/>>
internal sealed class NpgsqlConnectionFactory(string connectionString) : IDbConnectionFactory
{
    /// <inheritdoc/>>
    public IDbConnection CreateConnection()
    {
        var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        return connection;
    }
}