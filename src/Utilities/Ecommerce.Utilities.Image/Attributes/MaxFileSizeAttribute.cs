using Ecommerce.Utilities.Image.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Utilities.Image.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IEnumerable<IFormFile> files)
            {
                foreach (var file in files)
                {
                    if (file.Length > _maxFileSize)
                    {
                        return new ValidationResult(string.Format(Messagge.ErrorMaxFileSize, _maxFileSize));
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}