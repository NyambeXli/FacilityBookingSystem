using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UfsConnectBook.Data;
using UfsConnectBook.Models.Entities;
using UfsConnectBook.Models.ViewModel;
using DinkToPdf;
using DinkToPdf.Contracts;
using System.IO;
using UfsConnectBook.Utilities;
using NPOI;
using System.Text;
using UfsConnectBook.Models;

namespace UfsConnectBook.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministratorController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext appDbContext;
        //private readonly IPdfGenerator _pdfGenerator;

        public AdministratorController(UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager,
        AppDbContext appDbContext)
        //IPdfGenerator pdfGenerator Inside the ctor
        {
            _userManager = userManager;
            _roleManager = roleManager;
            this.appDbContext = appDbContext;
            //_pdfGenerator = pdfGenerator;
        }


        public async Task<IActionResult> Index(string Message)
        {

            if (!string.IsNullOrWhiteSpace(Message))
                ViewBag.Message = Message;
            var users = appDbContext.Users.ToList();
            List<AppUser> managerUsers = new List<AppUser>();
            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "Manager"))
                    managerUsers.Add(user);
            }
            return View(managerUsers);
        }
        public IActionResult CreateManager()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateManager(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (ModelState.IsValid)
            {
                var user =
                new AppUser
                {
                    Email = model.EmailAddress,
                    UserName = model.EmailAddress,
                    Name = model.Name,
                    Surname = model.Surname,
                    Identity = model.Identification
                };
                var result = await _userManager.CreateAsync(user,
                model.Password);

                if (result.Succeeded)
                {
                    string role = "Manager";

                    if (!await _roleManager.RoleExistsAsync(role))
                        await _roleManager.CreateAsync(new IdentityRole { Name = role });

                    await _userManager.AddToRoleAsync(user, role);
                    return RedirectToAction(nameof(Index),
                        new { Message = $"New Manager Created Successful. Email: {model.EmailAddress}. Password: {model.Password}" });
                }
            }
            ModelState.AddModelError("", "Failed to register manager user!");
            ViewBag.IsModelError = false;
            return View(model);
        }
        public IActionResult Orders(string Message)
        {


            if (!string.IsNullOrWhiteSpace(Message))
                ViewBag.Message = Message;

            var booking = appDbContext.Bookings.Where(s => s.userEmail!
            .Equals(User.Identity.Name) && s.Status != "Canceled").OrderByDescending(s => s.Id).ToList();
            for (int i = 0; i < booking.Count(); i++)
            {
                booking[i].Facility = appDbContext.Facilities.Find(booking[i].FacilityId);
            }
            return View(booking);
        }
        public async Task<IActionResult> Incharge(string Message)
        {


            if (!string.IsNullOrWhiteSpace(Message))
                ViewBag.Message = Message;
            var users = appDbContext.Users.ToList();
            List<AppUser> inchargeUsers = new List<AppUser>();
            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "Incharge"))
                    inchargeUsers.Add(user);
            }
            return View(inchargeUsers);
        }
        public async Task<IActionResult> Users(string Message)
        {


            if (!string.IsNullOrWhiteSpace(Message))
                ViewBag.Message = Message;
            var users = appDbContext.Users.ToList();
            List<AppUser> appUsers = new List<AppUser>();
            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "AppUser"))
                    appUsers.Add(user);
            }
            return View(appUsers);
        }
        public IActionResult AdminBookingHistory()
        {
            return View();
        }
        public IActionResult GeneratePdfReport()
        {
            var pdfContent = "Report generated";
            var pdfBytes = Encoding.UTF8.GetBytes(pdfContent);
            return File(pdfBytes, "application/pdf", "Report.pdf");

        }
        public IActionResult GenerateBookingHistoryReport()
        {
            var bookings = appDbContext.Bookings
                .Where(s => s.Status != "Canceled")
                .OrderByDescending(s => s.BookingDate)
                .ToList();
            return View("GenerateBookingHistoryReport", bookings);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteManagerUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("DeleteManagerUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDeleteManagerUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index), new { Message = $"Manager user {user.Email} has been deleted successfully." });
            }


            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> EditManagerUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var model = new EditUserModel
            {
                UserId = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                EmailAddress = user.Email,
                Identification = user.Identity
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditManagerUser(EditUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                return NotFound();
            }

            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = model.EmailAddress;
            user.Identity = model.Identification;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index), new { Message = $"Manager user {user.Email} has been updated successfully." });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }


    }
}

