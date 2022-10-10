using System.Text.Encodings.Web;
using System.Text.Json;

namespace ECommerce.Shared.Serializers
{
    public static class AppJsonSerializerOptions
    {
        public static JsonSerializerOptions CamelCase
        {
            get
            {
                return new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
            }
        }
    }
}