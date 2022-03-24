// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Therania.Data;

namespace Therania.Areas.Identity.Pages.Account
{
    public class RegisterTherapistModel : PageModel
    {
        private readonly SignInManager<Therapist> _signInManager;
        private readonly UserManager<Therapist> _userManager;
        private readonly IUserStore<Therapist> _userStore;
        private readonly IUserEmailStore<Therapist> _emailStore;
        private readonly ILogger<RegisterTherapistModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterTherapistModel(
            UserManager<Therapist> userManager,
            IUserStore<Therapist> userStore,
            SignInManager<Therapist> signInManager,
            ILogger<RegisterTherapistModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Full Name")]
            public string FullName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Age")]
            public DateOnly Age { get; set; }
            
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "License Type")]
            public string LicenseType { get; set; }
            
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "License Number")]
            public string LicenseNumber { get; set; }
            
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Country")]
            public string Country { get; set; }
            
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Governorate")]
            public string Governorate { get; set; }
            
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Mobile Number")]
            public string MobileNumber { get; set; }
            
            [Required]
            [DataType(DataType.Upload)]
            [Display(Name = "Profile Picture")]
            public string ProfilePicture { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var claims = new List<Claim>();
                    var fullNameClaim = new Claim("FullName", Input.FullName);
                    var age = new Claim("Age", Input.Age.ToString());
                    claims.Add(fullNameClaim);
                    await _userManager.AddClaimAsync(user, new Claim("FullName", Input.FullName));
                    await _userManager.AddClaimAsync(user, new Claim("Age", Input.Age.ToString()));
                    await _userManager.AddClaimAsync(user, new Claim("LicenseType", Input.LicenseType));
                    await _userManager.AddClaimAsync(user, new Claim("LicenseNumber", Input.LicenseNumber));
                    await _userManager.AddClaimAsync(user, new Claim("Country", Input.Country));
                    await _userManager.AddClaimAsync(user, new Claim("Governorate", Input.Governorate));
                    await _userManager.AddClaimAsync(user, new Claim("MobileNumber", Input.MobileNumber));
                    await _userManager.AddClaimAsync(user, new Claim("ProfilePicture", Input.ProfilePicture));

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private Therapist CreateUser()
        {
            try
            {
                return Activator.CreateInstance<Therapist>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(Therapist)}'. " +
                    $"Ensure that '{nameof(Therapist)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<Therapist> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<Therapist>)_userStore;
        }
    }
}
