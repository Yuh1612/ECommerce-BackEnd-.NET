using ECommerce.Shared.Serializers;

namespace ECommerce.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object @object)
        {
            return JsonSerializerHelper.DefaultSerialize(@object);
        }
    }
}