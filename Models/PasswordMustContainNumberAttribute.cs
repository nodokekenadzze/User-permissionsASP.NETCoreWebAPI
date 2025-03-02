using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UserPermissionsApi.Models
{
    public class PasswordMustContainNumberAttribute : ValidationAttribute
    {
        public PasswordMustContainNumberAttribute()
            : base("Password must contain at least one number.")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = value as string;
            
            if (!Regex.IsMatch(password, @"\d"))
            {
                return new ValidationResult("Password must contain at least one number.");
            }

            return ValidationResult.Success;; 
        }
    }
}
