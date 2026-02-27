using System.Data;
using System.Text.Json.Nodes;
using Dapper;
using Npgsql;

namespace DatabaseLib.DapperExtensions.TypeHandlers;

/// <summary>
/// Обработчик типа для маппинга JSONB полей PostgreSQL в JsonObject и обратно
/// Позволяет Dapper автоматически сериализовать/десериализовать JSON данные
/// </summary>
public class JsonObjectTypeHandler : SqlMapper.TypeHandler<JsonObject?>
{
    /// <summary>
    /// Преобразует JsonObject в значение для параметра SQL запроса
    /// </summary>
    public override void SetValue(IDbDataParameter parameter, JsonObject? value)
    {
        parameter.Value = value?.ToJsonString() ?? (object)DBNull.Value;
        if (parameter is NpgsqlParameter npgsqlParameter)
        {
            npgsqlParameter.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb;
        }
    }

    /// <summary>
    /// Преобразует значение из базы данных обратно в JsonObject
    /// </summary>
    public override JsonObject? Parse(object? value)
    {
        if (value is null or DBNull)
        {
            return null;
        }
            
        var jsonString = value.ToString();
        return JsonNode.Parse(jsonString) as JsonObject;
    }
}