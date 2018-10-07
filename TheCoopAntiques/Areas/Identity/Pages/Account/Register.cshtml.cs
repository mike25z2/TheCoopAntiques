using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TheCoopAntiques.Data;
using TheCoopAntiques.Models;
using TheCoopAntiques.Models.ViewModel;
using TheCoopAntiques.Utility;

namespace TheCoopAntiques.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _db = db;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name="First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            public string Role { get; set; }

        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null )
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                int defaultDealerID = _db.Dealers.First(d => d.Name == "None").Id;
                var userVM = new ApplicationUserViewModel
                {
                    Dealers = _db.Dealers.Where(d => d.Disabled == false).ToList(),
                    ApplicationUsers = new Models.ApplicationUser()
                    {
                        UserName = Input.Email,
                        Email = Input.Email,
                        FirstName = Input.FirstName,
                        LastName = Input.PhoneNumber,
                        PhoneNumber = Input.PhoneNumber,
                        DealerId = defaultDealerID
                    }
                };

                var result = await _userManager.CreateAsync(userVM.ApplicationUsers, Input.Password);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(SD.AdminUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.AdminUser));
                    }
                    if (!await _roleManager.RoleExistsAsync(SD.Owner))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.Owner));
                    }
                    if (!await _roleManager.RoleExistsAsync(SD.Volunteer))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.Volunteer));
                    }
                    if (!await _roleManager.RoleExistsAsync(SD.DealerV))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.DealerV));
                    }
                    if (!await _roleManager.RoleExistsAsync(SD.DealerNV))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.DealerNV));
                    }
                    if (!await _roleManager.RoleExistsAsync(SD.Customer))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.Customer));
                    }
                    _logger.LogInformation("User created a new account with password.");

                    // Adding Role
                    switch (Input.Role)
                    {
                        case "Admin":
                            await _userManager.AddToRoleAsync(userVM.ApplicationUsers, SD.AdminUser);
                            break;
                        case "Owner":
                            await _userManager.AddToRoleAsync(userVM.ApplicationUsers, SD.Owner);
                            break;
                        case "Volunteer":
                            await _userManager.AddToRoleAsync(userVM.ApplicationUsers, SD.Volunteer);
                            break;
                        case "DealerV":
                            await _userManager.AddToRoleAsync(userVM.ApplicationUsers, SD.DealerV);
                            break;
                        case "DealerNV":
                            await _userManager.AddToRoleAsync(userVM.ApplicationUsers, SD.DealerNV);
                            break;
                        default:
                            await _userManager.AddToRoleAsync(userVM.ApplicationUsers, SD.Customer);
                            break;
                    }

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(userVM.ApplicationUsers);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = userVM.ApplicationUsers.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(userVM.ApplicationUsers, isPersistent: false);
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
    }
}
