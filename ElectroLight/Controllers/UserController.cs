using ElectroLight.Application.Utilies;
using ElectroLight.Domain.Entities;
using ElectroLight.ViewsModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElectroLight.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // ========================= INDEX =========================
        public IActionResult Index()
        {
            return View();
        }

        // ========================= GET UPDATE =========================
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

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

        // ========================= POST UPDATE =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
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

            var currentUserId = _userManager.GetUserId(User);

            // Prevent admin from removing his own admin role
            if (currentUserId == userVm.Id &&
                userVm.Role != SD.Role_Admin)
            {
                TempData["error"] = "You cannot remove your own admin role.";

                userVm.Roles = _roleManager.Roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                });

                return View(userVm);
            }

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

            // Update User Role
            var currentRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            await _userManager.AddToRoleAsync(user, userVm.Role);

            TempData["success"] = "User updated successfully.";

            return RedirectToAction(nameof(Index));
        }

        // ========================= GET ALL USERS =========================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = _userManager.Users.ToList();

            var usersVM = new List<UserVM>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                usersVM.Add(new UserVM
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Role = string.Join(", ", roles)
                });
            }

            return Json(new { data = usersVM });
        }

        // ========================= DELETE USER =========================
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new
                {
                    success = false,
                    message = "Invalid user id."
                });
            }

            // Current Logged In Admin ID
            var currentUserId = _userManager.GetUserId(User);

            // Prevent self delete
            if (currentUserId == id)
            {
                return Json(new
                {
                    success = false,
                    message = "You cannot delete your own account."
                });
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return Json(new
                {
                    success = false,
                    message = "User not found."
                });
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return Json(new
                {
                    success = false,
                    message = "Error while deleting user."
                });
            }

            TempData["success"] = "User deleted successfully.";

            return Json(new
            {
                success = true,
                message = "User deleted successfully."
            });
        }
    }
}