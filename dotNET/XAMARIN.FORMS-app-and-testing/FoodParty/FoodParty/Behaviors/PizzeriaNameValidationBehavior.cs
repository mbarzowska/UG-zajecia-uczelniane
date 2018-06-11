using FoodParty.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace FoodParty.Behaviors
{
    public class PizzeriaNameValidationBehavior : BaseValidationBehavior
    {
        protected override void BindableOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            var name = textChangedEventArgs.NewTextValue;
            var nameEntry = sender as Entry;

            if (Regex.IsMatch(name, Validator.PizzeriaNamePattern))
                nameEntry.TextColor = Color.Black;
            else
                nameEntry.TextColor = Color.Red;
        }
    }
}
