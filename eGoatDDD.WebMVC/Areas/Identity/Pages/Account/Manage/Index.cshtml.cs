using eGoatDDD.Application.AppUsers.Commands;
using eGoatDDD.Application.AppUsers.Models;
using eGoatDDD.Application.AppUsers.Queries;
using eGoatDDD.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;

namespace eGoatDDD.WebMVC.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        #region ++g++ custom update
        // IdentityUser -> ApplicationUser
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMediator _mediator;
        #endregion

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;


        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            IMediator mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _mediator = mediator;
        }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            #region ++g++ custom added
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

            #endregion

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            #region ++g++ custom added
            CancellationToken cancellationToken;
            AppUserViewModel entity = await _mediator.Send(new GetAppUserQuery(user.Id), cancellationToken);
            #endregion

            Input = new InputModel
            {
                #region ++g++ custom added
                Role = entity.AppUser.Role,
                FirstName = entity.AppUser.FirstName,
                MiddleName = entity.AppUser.MiddleName,
                LastName = entity.AppUser.LastName,
                HomeAddress = entity.AppUser.HomeAddress,
                HomeCity = entity.AppUser.HomeCity,
                HomeRegion = entity.AppUser.HomeRegion,
                HomeCountryCode = entity.AppUser.HomeCountryCode,
                #endregion

                Email = email,
                PhoneNumber = phoneNumber
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);


            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            #region ++g++ custom added
            AppUserDto payLoad = new AppUserDto
            {
                AppUserId = user.Id,
                Role = Input.Role,
                FirstName = Input.FirstName,
                MiddleName = Input.MiddleName,
                LastName = Input.LastName,
                HomeAddress = Input.HomeAddress,
                HomeCity = Input.HomeCity,
                HomeRegion = Input.HomeRegion,
                HomeCountryCode = Input.HomeCountryCode,
                HomePhone = Input.PhoneNumber,
                IsActivated = 0,
                Joined = DateTime.Now,
                PackageId = 1,
            };

            CancellationToken cancellationToken;

            AppUserDto AppUser = await _mediator.Send(new UpdateAppUserCommand(payLoad), cancellationToken);
            #endregion


            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            #region ++g++ custom added
            await _userManager.UpdateAsync(user);
            #endregion

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}
