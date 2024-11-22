using System.ComponentModel.DataAnnotations;

namespace Rumo.Validation;

public class UppercaseAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string stringValue)
        {
            stringValue = stringValue.ToUpper();
            var property = validationContext.ObjectType.GetProperty(validationContext.MemberName);
            if (property != null)
            {
                property.SetValue(validationContext.ObjectInstance, stringValue, null);
            }
        }
        return ValidationResult.Success;
    }
}
