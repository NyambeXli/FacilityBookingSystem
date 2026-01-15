using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UfsConnectBook.Data;
using UfsConnectBook.Models.Entities;
using UfsConnectBook.Models.ViewModel;
using UfsConnectBook.Models;
using Microsoft.AspNetCore.Authorization;

namespace UfsConnectBook.Controllers
{

    public class ManagerController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext appDbContext;
        private readonly IRepositoryWrapper wrapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ManagerController(UserManager<AppUser> userManager,
        AppDbContext appDbContext,
        RoleManager<IdentityRole> roleManager,
        IRepositoryWrapper wrapper)
        {
            _userManager = userManager;
            this.appDbContext = appDbContext;
            _roleManager = roleManager;
            this.wrapper = wrapper;
        }
        [ActionName("Index")]
        public async Task<IActionResult> Index(string Message)
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
        public IActionResult CreateIncharge()
        {


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateIncharge(RegisterModel model)
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
                    string role = "Incharge";

                    if (!await _roleManager.RoleExistsAsync(role))
                        await _roleManager.CreateAsync(new IdentityRole { Name = role });

                    await _userManager.AddToRoleAsync(user, role);
                    return RedirectToAction(nameof(Index),
                        new { Message = $"New Offier InCharge Created Successful. Email: {model.EmailAddress}. Password: {model.Password}" });
                }
            }
            ModelState.AddModelError("", "Failed to register user!");
            ViewBag.IsModelError = false;
            return View(model);
        }
        public IActionResult Bookings(string Message)
        {
            if (!string.IsNullOrEmpty(Message))
                ViewBag.Message = Message;


            var bookings = appDbContext.Bookings.ToList();
            for (int i = 0; i < bookings.Count(); i++)
                bookings[i].Facility = appDbContext.Facilities.Find(bookings[i].FacilityId);
            return View(bookings);
        }
        public IActionResult Approve(int bookingId)
        {

            return View(appDbContext.Bookings.Find(bookingId));
        }
        [HttpPost]
        public IActionResult Approve(int bookingId, Booking booking)
        {

            var _booking = appDbContext.Bookings.Find(bookingId);
            _booking!.Status = "Aproved";
            appDbContext.Bookings.Update(_booking);
            appDbContext.SaveChanges();
            TempData["BookingApproved"] = "Your booking has been approved.";
            TempData["NewNotification"] = true;
            return RedirectToAction(nameof(Index), new { Message = $"The booking for {_booking.userEmail} was approved and is now in progress!" });
        }
        public IActionResult Facilities()
        {
            var facilities = appDbContext.Facilities.ToList();
            for (int i = 0; i < facilities.Count(); i++)
            {
                facilities[i].Category = appDbContext.Category.Find(facilities[i].CategoryId);
            }
            return View(facilities);
        }
        [HttpGet]
        public IActionResult Review()
        {
            IEnumerable<FeedBack> feedBacks = (IEnumerable<FeedBack>)wrapper.Review.GetAll();

            return View(feedBacks);
        }
        //return View();
        [HttpGet]
        public IActionResult ChangePasswordManager()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePasswordManager(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
            {
                return NotFound();
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (changePasswordResult.Succeeded)
            {

                return RedirectToAction(nameof(Bookings), new { Message = "Password changed successfully." });
            }
            else
            {

                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }
        }
        public IActionResult UsersWithPayments()
        {
            var usersWithPayments = appDbContext.Bookings
                .Where(b => b.Status == "Approved")
                .Select(b => b.userEmail)
                .Distinct()
                .ToList();
            return View(usersWithPayments);
        }

        public IActionResult UsersWithoutPayments()
        {
            var usersWithoutPayments = appDbContext.Bookings
                .Where(b => b.Status == "Pending" || b.Status == "Confirmed")
                .Select(b => b.userEmail)
                .Distinct()
                .ToList();
            return View(usersWithoutPayments);
        }


    }
}