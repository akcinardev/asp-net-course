using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ModelValidationExample.CustomValidators
{
    public class DateRangeValidatorAttribute : ValidationAttribute
    {
        public string OtherPropName { get; set; }

        public DateRangeValidatorAttribute(string otherPropName)
        {
            OtherPropName = otherPropName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value != null)
            {
                DateTime to_date = Convert.ToDateTime(value);

                PropertyInfo? otherPropInfo = validationContext.ObjectType.GetProperty(OtherPropName);  
                if(otherPropInfo != null)
                {
                    DateTime from_date = Convert.ToDateTime(otherPropInfo.GetValue(validationContext.ObjectInstance));

                    if (from_date > to_date)
                    {
                        return new ValidationResult(ErrorMessage, new string[] { OtherPropName, validationContext.MemberName });
                    }
                }
                return null;
            }
            return null;
        }
    }
}
