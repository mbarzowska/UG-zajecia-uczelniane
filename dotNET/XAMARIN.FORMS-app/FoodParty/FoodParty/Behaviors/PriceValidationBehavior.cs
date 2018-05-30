using FoodParty.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace FoodParty.Behaviors
{
    public class PriceValidationBehavior : BaseValidationBehavior
    {
        protected override void BindableOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            var price = textChangedEventArgs.NewTextValue;
            var priceEntry = sender as Entry;

            if (Regex.IsMatch(price, Validator.PricePattern) && !Regex.IsMatch(price, Validator.ZeroWithDotPattern))
                priceEntry.TextColor = Color.Black;
            else
                priceEntry.TextColor = Color.Red;
        }
    }
}
