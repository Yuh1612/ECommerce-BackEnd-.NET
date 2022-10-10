using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Shared.Serializers
{
    public static class JsonSerializerHelper
    {
        public static T? Deserialize<T>(string jsonString, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Deserialize<T>(jsonString, options ?? AppJsonSerializerOptions.CamelCase);
        }

        public static object? Deserialize(string jsonString, Type type)
        {
            return JsonSerializer.Deserialize(jsonString, type, AppJsonSerializerOptions.CamelCase);
        }

        public static string Serialize(object data, Type? dataType = default, JsonSerializerOptions? options = null)
        {
            dataType ??= data.GetType();
            return JsonSerializer.Serialize(data, dataType, options ?? AppJsonSerializerOptions.CamelCase);
        }

        public static T? DefaultDeserialize<T>(string jsonString)
        {
            return JsonSerializer.Deserialize<T>(jsonString);
        }

        public static object? DefaultDeserialize(string jsonString, Type type)
        {
            return JsonSerializer.Deserialize(jsonString, type);
        }

        public static string DefaultSerialize(object data, Type? dataType = default)
        {
            dataType ??= data.GetType();
            return JsonSerializer.Serialize(data, dataType);
        }
    }
}