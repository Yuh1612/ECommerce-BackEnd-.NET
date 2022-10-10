using System.ComponentModel.DataAnnotations;

namespace ECommerce.Shared.Attributes
{
    public class RequiredIfAttribute : RequiredAttribute
    {
        private readonly string _Propety;
        private readonly object?[] _Values;

        public RequiredIfAttribute(string propety, object?[] values)
        {
            _Propety = propety;
            _Values = values ?? new object?[] { null };
        }

        public RequiredIfAttribute(string otherPropety, object value)
        {
            _Propety = otherPropety;
            _Values = new object[] { value };
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var otherProperty = validationContext.ObjectInstance
                .GetType()
                .GetProperty(_Propety);

            if (otherProperty != null)
            {
                var otherValue = otherProperty.GetValue(validationContext.ObjectInstance);
                if (_Values.Contains(otherValue)) return base.IsValid(value, validationContext);
            }

            return ValidationResult.Success;
        }
    }
}