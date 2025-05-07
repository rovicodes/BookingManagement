using BookingManagement.Application.Common.Infrastructure;
using BookingManagement.Application.Common.Utility;
using BookingManagement.Domain.Entities;
using BookingManagement.Domain.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookingManagement.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Login(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            LoginVM loginVM = new ()
            {
                RedirectUrl = returnUrl
            };

            return View(loginVM);
        }

        public IActionResult Register(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            RegisterVM registerVM = new RegisterVM
            {
                RedirectUrl = returnUrl,
                RoleList = _roleManager.Roles.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Name
                }).ToList()
            };

            return View(registerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
           if(ModelState.IsValid) 
           { 
            ApplicationUser appUser = new()
            {
                FullName = registerVM.Name,
                PhoneNumber = registerVM.PhoneNumber,
                Email = registerVM.Email,
                NormalizedEmail = registerVM.Email.ToUpper(),
                EmailConfirmed = true,
                UserName = registerVM.Email,
                CreatedAt = DateTime.Now
            };

            var result = await _userManager.CreateAsync(appUser, registerVM.Password); //IdentityResult : result will be object of type IdentityResult

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(registerVM.Role))
                {
                    await _userManager.AddToRoleAsync(appUser, registerVM.Role);
                }
                else
                {
                    await _userManager.AddToRoleAsync(appUser, AppConstants.Role_Customer);
                }

                await _signInManager.SignInAsync(appUser, isPersistent: false);

                if (!string.IsNullOrEmpty(registerVM.RedirectUrl))
                {
                    return LocalRedirect(registerVM.RedirectUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
           }
            registerVM.RoleList = _roleManager.Roles.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Name
            }).ToList();

            return View(registerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            var appUser = await _userManager.FindByEmailAsync(loginVM.Email);

            if(appUser != null && await _userManager.CheckPasswordAsync(appUser , loginVM.Password))
            {
                await _signInManager.SignInAsync(appUser, isPersistent: false);

                if(!string.IsNullOrEmpty(loginVM.RedirectUrl))
                {
                    return LocalRedirect(loginVM.RedirectUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid credentials");

            return View(loginVM);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
