using ECommerce.Shared.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static string CloneToJsonObject(this object @object)
        {
            return JsonSerializerHelper.DefaultSerialize(@object);
        }
    }
}