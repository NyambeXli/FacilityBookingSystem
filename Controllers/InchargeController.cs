using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using UfsConnectBook.Data;
using UfsConnectBook.Models.Entities;

namespace UfsConnectBook.Controllers
{

    public class InchargeController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly AppDbContext appDbContext;

        public InchargeController(AppDbContext appDbContext, UserManager<AppUser> userManager)
        {
            this.appDbContext = appDbContext;
            this.userManager = userManager;
        }
        public IActionResult Index(string Message)
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
            _booking!.Status = "Approved";
            appDbContext.Bookings.Update(_booking);
            appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index), new { Message = $"The booking for {_booking.userEmail} was approved and is now in progress, The status is now APPROVED!" });
        }
        //public IActionResult BookingHistory()
        //{
        //    var bookings = appDbContext.Bookings
        //        .Where(s => s.Status != "Canceled")
        //        .OrderByDescending(s => s.BookingDate)
        //        .ToList();

        //    // Pass the bookings data to the Report view
        //    return View("BookingHistory", bookings);
        //}

        public async Task<IActionResult> BookingHistoryAsync()
        {
            var currentUserRoles = userManager.GetRolesAsync(await userManager.GetUserAsync(User)).Result;
            var bookings = new List<Booking>();

            if (currentUserRoles.Contains("InchargeG"))
            {
                bookings = appDbContext.Bookings
                    .Where(b => b.Status != "Canceled" && b.Facility.Category.Name == "Gym")
                    .OrderByDescending(b => b.BookingDate)
                    .ToList();
            }
            else if (currentUserRoles.Contains("InchargeL"))
            {
                bookings = appDbContext.Bookings
                    .Where(b => b.Status != "Canceled" && b.Facility.Category.Name == "Laundry")
                    .OrderByDescending(b => b.BookingDate)
                    .ToList();
            }
            else if (currentUserRoles.Contains("InchargeP"))
            {
                bookings = appDbContext.Bookings
                    .Where(b => b.Status != "Canceled" && b.Facility.Category.Name == "Parking")
                    .OrderByDescending(b => b.BookingDate)
                    .ToList();
            }
            else if (currentUserRoles.Contains("InchargeS"))
            {
                bookings = appDbContext.Bookings
                    .Where(b => b.Status != "Canceled" && b.Facility.Category.Name == "Study")
                    .OrderByDescending(b => b.BookingDate)
                    .ToList();
            }

            return View("BookingHistory", bookings);
        }

    }
}
