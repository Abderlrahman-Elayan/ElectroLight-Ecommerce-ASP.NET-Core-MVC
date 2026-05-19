using ElectroLight.Application.Utilies;
using ElectroLight.Domain.Entities;
using ElectroLight.ViewsModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ElectroLight.Controllers
{
    [Authorize]
    public class UserController: Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var roles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            });

            var userRoles = await _userManager.GetRolesAsync(user);

            UserVM userVM = new UserVM
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = userRoles.FirstOrDefault(),
                Roles = roles
            };

            return View(userVM);
        }


        [HttpPost]
        public async Task<IActionResult> Update(UserVM userVm)
        {
            if (!ModelState.IsValid)
            {
                userVm.Roles = _roleManager.Roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                });

                return View(userVm);
            }

            var user = await _userManager.FindByIdAsync(userVm.Id);

            if (user == null)
                return NotFound();

            user.FullName = userVm.FullName;
            user.Email = userVm.Email;
            user.UserName = userVm.Email;
            user.PhoneNumber = userVm.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                userVm.Roles = _roleManager.Roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                });

                return View(userVm);
            }

            var currentRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            await _userManager.AddToRoleAsync(user, userVm.Role);

            TempData["success"] = "User updated successfully";

            return RedirectToAction(nameof(Index));
        }

        #region APi

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["error"] = "cant delete User";
                return Json(new { success = false, message = "Error while deleting" });

            }

            await _userManager.DeleteAsync(user);

            TempData["success"] = "The User has been Deleted successfully.";

            return Json(new { success = true, message = "User has been Deleted Successfuly" });
        }
        #endregion

        public async Task<IActionResult> GetAll()
        {
            var users = _userManager.Users.ToList();

            var usersVM = new List<UserVM>();
            foreach(var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var userVM = new UserVM
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Role = string.Join(", ",roles)
                };
                usersVM.Add(userVM);
            }

          
            return Json(new { data = usersVM });
        }

    }
}
