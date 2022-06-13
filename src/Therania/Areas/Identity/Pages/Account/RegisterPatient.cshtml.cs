// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Therania.Data;
using Therania.Models;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Therania.Areas.Identity.Pages.Account
{
    public class RegisterPatientModel : PageModel
    {
        private readonly SignInManager<Patient> _signInManager;
        private readonly UserManager<Patient> _userManager;
        private readonly IUserStore<Patient> _userStore;
        private readonly IUserEmailStore<Patient> _emailStore;
        private readonly ILogger<RegisterPatientModel> _logger;
        private readonly IEmailSender _emailSender;
        private IWebHostEnvironment _env;

        public RegisterPatientModel(
            UserManager<Patient> userManager,
            IUserStore<Patient> userStore,
            SignInManager<Patient> signInManager,
            ILogger<RegisterPatientModel> logger,
            IEmailSender emailSender,
            IWebHostEnvironment env)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _env = env;
        }

        [BindProperty] public PatientInputModel Input { get; set; }

        public List<Country> Countries { get; set; }
        public List<MentalHealthDisease> MentalHealthDiseases { get; set; }
        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            await PopulateCountriesObject();
            await PopulateMentalHealthDiseases();
        }


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            // var errors = ModelState.Values.SelectMany(v => v.Errors);
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var userWithSameEmail = await _userManager.FindByEmailAsync(Input.Email);
                var patient = CreatePatient();
                await _userStore.SetUserNameAsync(patient, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(patient, Input.Email, CancellationToken.None);


                var result = await _userManager.CreateAsync(patient, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await AddPatientClaims(patient);

                    var userId = await _userManager.GetUserIdAsync(patient);
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
                    await _signInManager.SignInAsync(patient, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            await PopulateCountriesObject();
            await PopulateMentalHealthDiseases();
            return Page();
        }


        private async Task PopulateCountriesObject()
        {
            string countriesJson = await System.IO.File.ReadAllTextAsync(_env.WebRootPath + "\\json\\countries.json");
            Countries = JsonSerializer.Deserialize<List<Country>>(countriesJson);
        }

        private async Task PopulateMentalHealthDiseases()
        {
            string diseasesJson =
                await System.IO.File.ReadAllTextAsync(_env.WebRootPath + "\\json\\mentalHealthDiseases.json");
            MentalHealthDiseases = JsonSerializer.Deserialize<List<MentalHealthDisease>>(diseasesJson);
        }

        private async Task AddPatientClaims(Patient patient)
        {
            await _userManager.AddClaimAsync(patient, new Claim("FullName", Input.FullName));
            await _userManager.AddClaimAsync(patient, new Claim("Age", Input.Age));
            await _userManager.AddClaimAsync(patient, new Claim("MentalHealthDisease", Input.MentalHealthDisease));
            await _userManager.AddClaimAsync(patient, new Claim("Country", Input.Country));
            await _userManager.AddClaimAsync(patient, new Claim("Governorate", Input.Governorate));
            await _userManager.AddClaimAsync(patient, new Claim("MobileNumber", Input.MobileNumber));
            await _userManager.AddClaimAsync(patient, new Claim("ProfilePicture", Input.ProfilePicture));
        }


        private Patient CreatePatient()
        {
            try
            {
                return Activator.CreateInstance<Patient>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(Patient)}'. " +
                                                    $"Ensure that '{nameof(Patient)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                                                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<Patient> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }

            return (IUserEmailStore<Patient>) _userStore;
        }
    }
}