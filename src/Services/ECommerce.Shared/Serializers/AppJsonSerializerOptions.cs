using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

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