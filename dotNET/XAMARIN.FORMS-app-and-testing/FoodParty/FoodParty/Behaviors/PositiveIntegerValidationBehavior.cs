using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using FoodParty.Validators;

namespace FoodParty.Behaviors
{
    public class PositiveIntegerValidationBehavior : BaseValidationBehavior
    {
        protected override void BindableOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            var size = textChangedEventArgs.NewTextValue;
            var sizeEntry = sender as Entry;

            if (Regex.IsMatch(size, Validator.IntegerPattern))
                sizeEntry.TextColor = Color.Black;
            else
                sizeEntry.TextColor = Color.Red;
        }
    }
}
