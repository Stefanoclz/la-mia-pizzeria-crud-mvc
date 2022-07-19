using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.ValidationAttributes
{
    public class MoreThanFiveWordsValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string fieldValue = (string)value;
            string[] allWords = fieldValue.Split(' ');
            if (fieldValue == null || allWords.Count() <= 4)
            {
                return new ValidationResult("Il campo deve contenere almeno 5 parole");
            }
            return ValidationResult.Success;
        }

    }
}
