using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Therania.Utilities;

public class MinimumAgeAttribute : ValidationAttribute
{
    private int _minAge;
    public MinimumAgeAttribute(int minAge)
    {
        _minAge = minAge;
        ErrorMessage = $"you must be at least {minAge} years of age to register";
    }

    public override bool IsValid(object? value)
    {
        DateTime enteredDate;
        // tries to parse the string input value into a DateTime object
        if (DateTime.TryParse(value.ToString(), out enteredDate))
        {
            // if the entered date + the minimum age required is less than the current date, return true
            if (enteredDate.AddYears(_minAge) < DateTime.Now) return true;
        }

        return false;
    }

    // public override string FormatErrorMessage(string name)
    // {
    //     return string.Format(ErrorMessage, name, _minAge);
    // }
}