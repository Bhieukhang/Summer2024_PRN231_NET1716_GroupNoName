using System.ComponentModel.DataAnnotations;

namespace JewelrySalesSystem_NoName_FE.DTOs.Account
{
    public class MinAgeAttribute : ValidationAttribute
    {
        private readonly int _minAge;

        public MinAgeAttribute(int minAge)
        {
            _minAge = minAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateTime)
            {
                if (dateTime.AddYears(_minAge) > DateTime.Now)
                {
                    return new ValidationResult($"Phải đủ {_minAge} tuổi trở lên.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
