﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace BookStore.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly BookStoreDbContext _DbContext;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            BookStoreDbContext DbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _DbContext = DbContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Email alanı boş geçilemez.")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Şifre alanı boş geçilemez.")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "Parola ve onay parolası eşleşmiyor.")]
            public string ConfirmPassword { get; set; }
            [Required(ErrorMessage = "Kullanıcı adı alanı boş geçilemez.")]
            [StringLength(30, MinimumLength = 2, ErrorMessage = "Ad alanı min:2 - max: 30 karakter olabilir.")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Kullanıcı soyadı alanı boş geçilemez.")]
            [StringLength(30, MinimumLength = 2, ErrorMessage = "Soyad alanı min:2 - max: 30 karakter olabilir.")]
            public string Surname { get; set; }
            [Required(ErrorMessage = "Adres alanı boş geçilemez.")]
            public string Adress { get; set; }
            [Required(ErrorMessage = "Şehir alanı boş geçilemez.")]
            public string City { get; set; }
            [Required(ErrorMessage = "İlçe alanı boş geçilemez.")]
            public string District { get; set; }
            [Required(ErrorMessage = "Posta Kodu alanı boş geçilemez.")]
            [StringLength(5, MinimumLength = 5, ErrorMessage = "Geçersiz Posta Kodu !")]
            public string PostCode { get; set; }
            [Required(ErrorMessage = "Telefon numarası alanı boş geçilemez.")]
            [StringLength(10, MinimumLength = 10, ErrorMessage = "Geçersiz telefon numarası !")]
            public string PhoneNumber { get; set; }
            public string Role { get; set; }
            public IEnumerable<SelectListItem> RoleList { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            Input = new InputModel()
            {
                RoleList = _roleManager.Roles.Where(x => x.Name != UserRoles.Role_Individual)
                .Select(y => y.Name)
                .Select(z => new SelectListItem
                {
                    Text = z,
                    Value = z
                })
            };
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Adress = Input.Adress,
                    City = Input.City,
                    District = Input.District,
                    Name = Input.Name,
                    Surname = Input.Surname,
                    PhoneNumber = Input.PhoneNumber,
                    PostCode = Input.PostCode,
                    Role = Input.Role

                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Kullanıcı şifre ile yeni bir hesap oluşturdu.");
                    if (!await _roleManager.RoleExistsAsync(UserRoles.Role_Admin))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(UserRoles.Role_Admin));
                    }
                    if (!await _roleManager.RoleExistsAsync(UserRoles.Role_User))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(UserRoles.Role_User));
                    }
                    if (!await _roleManager.RoleExistsAsync(UserRoles.Role_Individual))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(UserRoles.Role_Individual));
                    }
                    if (user.Role==null)
                    {
                        await _userManager.AddToRoleAsync(user, UserRoles.Role_User);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, user.Role);
                    }
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "E-mailinizi onaylayın",
                        $"Lütfen hesabınızı şu tarihe kadar onaylayın: <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>onaylamak için buraya tıklayın</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        if (user.Role==null)
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return LocalRedirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index","User",new {Area="Admin"});
                        }
                        
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
    }
}
