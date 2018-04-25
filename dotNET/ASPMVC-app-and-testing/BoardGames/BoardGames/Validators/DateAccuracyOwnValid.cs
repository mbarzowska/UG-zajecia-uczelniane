using System;
using System.ComponentModel.DataAnnotations;
using BoardGames.Models;

namespace BoardGames.Validators {
    public class DateAccuracyOwnValid : ValidationAttribute {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            Publisher publisher = (Publisher)validationContext.ObjectInstance;

            if (publisher == null)
                throw new ArgumentException("Atribute not applied on publisher.");

            if (publisher.FoundingDate.Date > DateTime.Now.Date) {
                return new ValidationResult(GetErrorMessage(validationContext));
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage(ValidationContext validationContext) {
            if (!string.IsNullOrEmpty(this.ErrorMessage))
                return ErrorMessage;

            return "Check Founding Date field, the date of founding cannot be placed in the future.";
        }
    }
}
