using ElectroLight.Application.Interfaces.Common;
using ElectroLight.Application.Utilies;
using ElectroLight.Domain.Entities;
using ElectroLight.ViewsModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace ElectroLight.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManger,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this._userManger = userManger;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
        }


        public IActionResult Register(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            RegisterVM registerVM = new()
            {
                ReturnUrl = returnUrl,
                Roles = _roleManager.Roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
            };

            if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).Wait();
            }
            if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).Wait();
            }



            return View(registerVM);

        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            registerVM.Roles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            });

            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            ApplicationUser user = new()
            {
                FullName = registerVM.Name,
                Email = registerVM.Email,
                EmailConfirmed = true,
                UserName = registerVM.Email,
                PhoneNumber = registerVM.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
            };
            var result = await _userManger.CreateAsync(user, registerVM.Password);

            if(result.Succeeded)
            {
                if(!string.IsNullOrEmpty(registerVM.Role))
                {
                   await _userManger.AddToRoleAsync(user, registerVM.Role);
                }
                else
                {
                    await _userManger.AddToRoleAsync(user, SD.Role_Customer);
                }
            }
            else
            {

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

              
                return View(registerVM);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            if(!string.IsNullOrEmpty(registerVM.ReturnUrl))
            {
                return LocalRedirect(registerVM.ReturnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public IActionResult Login(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            LoginVM loginVM = new()
            {
                ReturnUrl = returnUrl
            };

            return View(loginVM);
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {

            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }



            var result = await _signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password, 
                loginVM.RememberMe, lockoutOnFailure:false);


            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(loginVM.ReturnUrl))
                {
                    return LocalRedirect(loginVM.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt");
              
            }

            return View(loginVM);
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction ("Index","Home");
        }

        public  IActionResult AccessDenied()
        {

            return View();
        }
    }
}
