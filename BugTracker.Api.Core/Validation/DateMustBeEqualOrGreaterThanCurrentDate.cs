
using System;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Api.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public class DateMustBeEqualOrGreaterThanCurrentDate : ValidationAttribute
    {
        /// <summary>
        ///  
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            DateTime dt = (DateTime)value;
            if (dt >= DateTime.UtcNow)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage ?? "Make sure your date is >= than today");
        }
    }
}
