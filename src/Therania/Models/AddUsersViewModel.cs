using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Therania.Data;

namespace Therania.Models;

public class AddUsersViewModel
{
    [EmailAddress]
    public string? Email { get; set; }   
    
    [DataType(DataType.Password)]
    public string? Password { get; set; }   
    
    [DataType(DataType.Password)]
    public string? ConfirmPassword { get; set; }

    public Therapist ToTherapist()
    {
        return new Therapist
        {
            Email = Email,
            PasswordHash = Password
        };
    }
}