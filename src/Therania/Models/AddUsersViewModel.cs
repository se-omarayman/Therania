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

    public TherapistUser ToTherapist()
    {
        return new TherapistUser
        {
            Email = Email,
            PasswordHash = Password
        };
    }
}