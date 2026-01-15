using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using UfsConnectBook.Data;
using UfsConnectBook.Models.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace UfsConnectBook.Controllers
{
    [Authorize(Roles = "Incharge, InchargeG, InchargeL, InchargeP, Inchages, InchargeS")]
    public class InChargeProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public InChargeProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppUser user)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                currentUser.Name = user.Name;
                currentUser.Surname = user.Surname;

                var result = await _userManager.UpdateAsync(currentUser);

                if (result.Succeeded)
                {
                    ViewBag.Message = "Profile updated successfully.";
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(user);
        }
    }
}
