using System.Data;
using System.Text.Json.Nodes;
using Dapper;
using Npgsql;

namespace DatabaseLib.DapperExtensions.TypeHandlers;

/// <summary>
/// Обработчик типа для маппинга JSONB полей PostgreSQL в JsonArray и обратно
/// Позволяет Dapper автоматически сериализовать/десериализовать JSON данные
/// </summary>
public class JsonArrayTypeHandler : SqlMapper.TypeHandler<JsonArray?>
{
    /// <summary>
    /// Преобразует JsonArray в значение для параметра SQL запроса
    /// </summary>
    public override void SetValue(IDbDataParameter parameter, JsonArray? value)
    {
        parameter.Value = value?.ToJsonString() ?? (object)DBNull.Value;
        if (parameter is NpgsqlParameter npgsqlParameter)
        {
            npgsqlParameter.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb;
        }
    }

    /// <summary>
    /// Преобразует значение из базы данных обратно в JsonArray
    /// </summary>
    public override JsonArray? Parse(object? value)
    {
        if (value is null or DBNull)
        {
            return null;
        }
            
        var jsonString = value.ToString();
        return JsonNode.Parse(jsonString) as JsonArray;
    }
}