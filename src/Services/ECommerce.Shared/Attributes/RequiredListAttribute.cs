using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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