using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolRecordSystem.Extensions
{

    public class ValidateNumber : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var numberCheck = Convert.ToInt64(value.ToString());
                return ValidationResult.Success;
            }
            catch
            {
                return new ValidationResult("Enter a Valid Number");
            }
        }
    }

    public class ValidateNpPhoneNumber : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var numberValueCheck = Convert.ToInt64(value.ToString());
                if (value.ToString().Length == 10)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Please Enter a Valid Phone Number");
                }
            }
            catch
            {
                return new ValidationResult("Enter a Valid Number For A Phone Number");
            }
        }
    }
}
