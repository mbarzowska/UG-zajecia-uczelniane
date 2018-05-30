using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using FoodParty.Validators;

namespace FoodParty.Behaviors
{
    public class PositiveDecimalValidationBehavior : BaseValidationBehavior
    {
        protected override void BindableOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            var weight = textChangedEventArgs.NewTextValue;
            var weightEntry = sender as Entry;

            if (Regex.IsMatch(weight, Validator.DecimalPattern) && !Regex.IsMatch(weight, Validator.ZeroWithDotPattern))
                weightEntry.TextColor = Color.Black;
            else
                weightEntry.TextColor = Color.Red;
        }
    }
}
