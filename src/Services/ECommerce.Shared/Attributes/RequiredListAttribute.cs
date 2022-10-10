using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Shared.Attributes
{
    public class RequiredListAttribute : RequiredAttribute
    {
        private readonly int _minimumRows;

        public RequiredListAttribute()
        {
        }

        public RequiredListAttribute(int minimumRows)
        {
            _minimumRows = minimumRows;
        }

        public override bool IsValid(object? value)
        {
            var list = value as IEnumerable;

            if (list != null)
            {
                int index = 0;
                foreach (var item in list)
                {
                    if (index >= _minimumRows - 1) return true;
                    index++;
                }
            }

            return false;
        }
    }
}