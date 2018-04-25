using System;
using System.ComponentModel.DataAnnotations;
using BoardGames.Models;

namespace BoardGames.Validators
{
    public class HowManyGamersOwnValid : ValidationAttribute {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            Game game = (Game)validationContext.ObjectInstance;

            if (game == null)
                throw new ArgumentException("Atribute not applied on game.");

            if (game.MinGamers > game.MaxGamers) {
                return new ValidationResult(GetErrorMessage(validationContext));
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage(ValidationContext validationContext) {
            if (!string.IsNullOrEmpty(this.ErrorMessage))
                return ErrorMessage;

            return "Check min gamers and max gamers fields, max cannot be less than min.";
        }
    }
}
