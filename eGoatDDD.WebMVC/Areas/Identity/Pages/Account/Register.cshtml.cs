using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace eGoatDDD.WebMVC.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly eGoatDDDDbContext _context;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            eGoatDDDDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;

            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            #region ++g++ added register field
            // role to be added to claims
            // during register, the user needs to decide 
            // on an appropriate role.

            // updating roles in the future is
            // dependable on policies
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Role")]
            public string Role { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Middle Name")]
            public string MiddleName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Home Address")]
            public string HomeAddress { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Home City")]
            public string HomeCity { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Home Region")]
            public string HomeRegion { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Home Country")]
            public string HomeCountryCode { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Home Phone")]
            public string HomePhone { get; set; }
            #endregion

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
        }

        public void OnGet(string returnUrl = null)
        {
            ViewData["Roles"] = _context.Roles.Select(r => r.Name).Where(r => r.ToLower() != "administrator").OrderBy(r => r).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {
                    UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    #region ++g++

                    AppUser appUser = new AppUser
                    {
                        Id = user.Id,
                        Role = Input.Role,
                        FirstName = Input.FirstName,
                        MiddleName = Input.MiddleName,
                        LastName = Input.LastName,
                        HomeAddress = Input.HomeAddress,
                        HomeCity = Input.HomeCity,
                        HomeRegion = Input.HomeRegion,
                        HomeCountryCode = Input.HomeCountryCode,
                        HomePhone = Input.HomePhone,
                        Joined = DateTime.Now,
                        IsActivated = 0,
                        PackageId = 1,
                    };

                    _context.AppUsers.Add(appUser);
                                        
                    await _userManager.AddToRoleAsync(user, Input.Role);

                    Claim claim = new Claim(ClaimTypes.Role, Input.Role);
                    await _userManager.AddClaimAsync(user, claim);
                    #endregion

                    _logger.LogInformation("User created a new account with password.");
                    
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    #region ++g++
                    // the claim has been updates, We need to change the cookie value for getting the updated claim
                    await _signInManager.SignOutAsync();
                    #endregion

                    await _signInManager.SignInAsync(user, isPersistent: false);
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
