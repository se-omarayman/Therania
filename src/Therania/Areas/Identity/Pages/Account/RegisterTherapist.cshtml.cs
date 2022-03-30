// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Therania.Data;
using Therania.Models;

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

        [BindProperty] public TherapistInputModel Input { get; set; }
        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

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
                var therapist = CreateTherapist();

                await _userStore.SetUserNameAsync(therapist, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(therapist, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(therapist, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await AddTherapistClaims(therapist);

                    var userId = await _userManager.GetUserIdAsync(therapist);
                    // var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    // code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    // var callbackUrl = Url.Page(
                    //     "/Account/ConfirmEmail",
                    //     pageHandler: null,
                    //     values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                    //     protocol: Request.Scheme);

                    // await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //     $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    // if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    // {
                    //     return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    // }
                    await _signInManager.SignInAsync(therapist, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private async Task AddTherapistClaims(Therapist therapist)
        {
            await _userManager.AddClaimAsync(therapist, new Claim("FullName", Input.FullName));
            await _userManager.AddClaimAsync(therapist, new Claim("Age", Input.Age));
            await _userManager.AddClaimAsync(therapist, new Claim("LicenseType", Input.LicenseType));
            await _userManager.AddClaimAsync(therapist, new Claim("LicenseNumber", Input.LicenseNumber));
            await _userManager.AddClaimAsync(therapist, new Claim("Country", Input.Country));
            await _userManager.AddClaimAsync(therapist, new Claim("Governorate", Input.Governorate));
            await _userManager.AddClaimAsync(therapist, new Claim("MobileNumber", Input.MobileNumber));
            await _userManager.AddClaimAsync(therapist, new Claim("ProfilePicture", Input.ProfilePicture));
        }


        private Therapist CreateTherapist()
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

            return (IUserEmailStore<Therapist>) _userStore;
        }
    }
}