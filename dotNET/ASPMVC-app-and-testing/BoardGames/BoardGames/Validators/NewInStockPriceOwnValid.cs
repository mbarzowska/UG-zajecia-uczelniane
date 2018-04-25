using System;
using System.ComponentModel.DataAnnotations;
using BoardGames.Models;

namespace BoardGames.Validators {
    public class NewInStockPriceOwnValid : ValidationAttribute {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            Game game = (Game)validationContext.ObjectInstance;

            DateTime limit = new DateTime(2018, 1, 1);
            if (game.ReleaseDate.Date >= limit && game.Price < 50) {
                return new ValidationResult(GetErrorMessage(validationContext));
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage(ValidationContext validationContext) {
            if (!string.IsNullOrEmpty(this.ErrorMessage))
                return ErrorMessage;

            return "A rental price of recently released game cannot be less than 50.";
        }
    }
}
