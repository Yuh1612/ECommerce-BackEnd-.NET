using Ecommerce.Utilities.Image.Constants;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Utilities.Image.Attributes
{
    public class AllowedFileAttribute : ValidationAttribute
    {
        private readonly FileType _fileType;

        public AllowedFileAttribute(FileType fileType)
        {
            _fileType = fileType;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (_fileType != FileType.None && value is IEnumerable<IFormFile> files)
            {
                switch (_fileType)
                {
                    case FileType.Image:
                        foreach (var file in files)
                        {
                            var extension = Path.GetExtension(file.FileName);
                            if (!FileExtension.ImageExtensions.Contains(extension.ToLower()))
                            {
                                return new ValidationResult(string.Format(Messagge.ErrorFileExtension, extension));
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
            return ValidationResult.Success;
        }
    }
}