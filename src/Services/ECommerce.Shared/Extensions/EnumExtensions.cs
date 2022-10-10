using System.ComponentModel;
using System.Reflection;

namespace ECommerce.Shared.Extensions
{
    public static class EnumExtensions
    {
        public static T? GetFieldAttribute<T>(this Enum @enum) where T : Attribute
        {
            var desAttr = @enum?.GetType().GetField(@enum.ToString())?.GetCustomAttribute<T>();
            return desAttr;
        }

        public static string? GetDescription(this Enum @enum)
        {
            var desAttr = @enum?.GetFieldAttribute<DescriptionAttribute>();
            return desAttr?.Description;
        }
    }
}