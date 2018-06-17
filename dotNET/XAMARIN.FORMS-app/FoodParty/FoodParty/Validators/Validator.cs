using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FoodParty.Validators 
{
    public class Validator
    {
        public const string PizzeriaNamePattern = "^[A-Za-z0-9\\s]{1,25}$";
        public const string PricePattern = @"^([0-9]+(?:[\.][0-9]{0,2})?|\.[0-9]{0,2})$";
        public const string DecimalPattern = @"^([0-9]+(?:[\.][0-9]*)?|\.[0-9]+)$";
        public const string IntegerPattern = @"^[1-9]\d*$";
        public const string ZeroPattern = @"^[0]*$";
        public const string ZeroWithDotPattern = @"^[0]+(\.[0]*)?$";

        public static bool ValidateString(string value, string pattern)
        {
            if (value == null || string.IsNullOrEmpty(value)) 
            { 
                return false;
            } 
            else if (Regex.IsMatch(value, pattern)) 
            { 
                return true;
            } 
            else 
            { 
                return false;
            }
        }

        public static bool ValidatePizzeria(string pizzeriaName)
        {
            return ValidateString(pizzeriaName, PizzeriaNamePattern);
        }

        public static bool ValidateInt(int value)
        {
            if (value <= 0) 
            { 
                return false;
            }
            return true;
        }

        public static bool ValidateDecimal(decimal value)
        {
            if (value <= 0.0m) 
            { 
                return false;
            }
            return true;
        }

        public static bool ValidatePrice(decimal value)
        {
            if (value <= 0.0m) 
            { 
                return false;
            }
            string val = value.ToString();
            return ValidateString(val, PricePattern);
        }
    }
}
