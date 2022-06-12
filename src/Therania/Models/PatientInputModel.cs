using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Therania.Utilities;

namespace Therania.Models;

        public class PatientInputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string? Email { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Full Name")]
            public string? FullName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string? Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string? ConfirmPassword { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [DisplayName("Date of Birth")]
            [MinimumAge(18)]
            public string? Age { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Country")]
            public string MentalHealthDisease { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Country")]
            public string? Country { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Governorate")]
            public string? Governorate { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Mobile Number")]
            public string? MobileNumber { get; set; }

            [Required]
            [DataType(DataType.Upload)]
            [Display(Name = "Profile Picture")]
            public string? ProfilePicture { get; set; }
        }
