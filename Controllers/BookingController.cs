using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UfsConnectBook.Data;
using UfsConnectBook.Models.Entities;
using Stripe;
using Stripe.Checkout;
using Microsoft.Extensions.Options;
using UfsConnectBook.Models;

namespace UfsConnectBook.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly AppDbContext appDbContext;
        private readonly UserManager<AppUser> userManager;
        private readonly IRepositoryWrapper wrapper;
        private readonly StripeSettings _Settings;

        public BookingController(AppDbContext appDbContext, UserManager<AppUser> userManager, IRepositoryWrapper wrapper, IOptions<StripeSettings> settings)
        {
            this.appDbContext = appDbContext;
            this.userManager = userManager;
            this.wrapper = wrapper;
            _Settings = settings.Value;
        }

        [HttpPost]
        public IActionResult ConfirmCancel(int bookingID)
        {
            var booking = appDbContext.Bookings.Find(bookingID);

            if (booking == null)
                return NotFound();

            booking.Status = "Canceled";
            appDbContext.SaveChanges();

            return RedirectToAction(nameof(Index), new { Message = "Booking was canceled successfully." });
        }

        public IActionResult Index(string Message)
        {
            if (!string.IsNullOrWhiteSpace(Message))
                ViewBag.Message = Message;

            var userEmail = User.Identity?.Name;

            if (string.IsNullOrEmpty(userEmail))
                return RedirectToAction("Login", "Account");

            var booking = appDbContext.Bookings
                .Where(s => s.userEmail == userEmail && s.Status != "Canceled")
                .OrderByDescending(s => s.Id)
                .ToList();

            foreach (var item in booking)
            {
                item.Facility = appDbContext.Facilities.Find(item.FacilityId);
            }

            return View(booking);
        }

        public IActionResult Create()
        {
            PopulateDDL();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Booking booking)
        {
            if (!ModelState.IsValid)
            {
                PopulateDDL();
                return View(booking);
            }

            if (booking.StartTime >= booking.EndTime)
            {
                ModelState.AddModelError(nameof(Booking.StartTime), "The booking duration is invalid");
                PopulateDDL();
                return View(booking);
            }

            var userEmail = User.Identity?.Name;

            if (string.IsNullOrEmpty(userEmail))
                return RedirectToAction("Login", "Account");

            booking.BookingDate = DateTime.Now;
            booking.Duration = booking.EndTime - booking.StartTime;
            booking.userEmail = userEmail;
            booking.Status = "Pending";

            appDbContext.Bookings.Add(booking);
            appDbContext.SaveChanges();

            return RedirectToAction(nameof(Index), new
            {
                Message = "Your new Booking has been made successful but has not yet been approved, Please Make booking payments"
            });
        }

        public IActionResult Details(int bookingID)
        {
            var booking = appDbContext.Bookings.Find(bookingID);

            if (booking == null)
                return NotFound();

            booking.Facility = appDbContext.Facilities.Find(booking.FacilityId);

            return View(booking);
        }

        public IActionResult Edit(int bookingID)
        {
            var booking = appDbContext.Bookings.Find(bookingID);

            if (booking == null)
                return NotFound();

            booking.Facility = appDbContext.Facilities.Find(booking.FacilityId);

            PopulateDDL();

            return View(booking);
        }

        [HttpPost]
        public IActionResult Edit(Booking booking)
        {
            if (!ModelState.IsValid)
            {
                PopulateDDL();
                return View(booking);
            }

            booking.Duration = booking.EndTime - booking.StartTime;

            appDbContext.Bookings.Update(booking);
            appDbContext.SaveChanges();

            return RedirectToAction(nameof(Index), new { Message = "Booking Updated Successfully.." });
        }

        public IActionResult Cancel(int bookingID)
        {
            var booking = appDbContext.Bookings.Find(bookingID);

            if (booking == null)
                return NotFound();

            booking.Facility = appDbContext.Facilities.Find(booking.FacilityId);

            return View(booking);
        }

        [HttpPost]
        public IActionResult Cancel(Booking booking)
        {
            var existingBooking = appDbContext.Bookings.Find(booking.Id);

            if (existingBooking == null)
                return NotFound();

            existingBooking.Status = "Canceled";

            appDbContext.Update(existingBooking);
            appDbContext.SaveChanges();

            return RedirectToAction(nameof(Index), new { Message = "Booking was deleted Successfully.." });
        }

        private void PopulateDDL()
        {
            ViewBag.FacilityId = new SelectList(appDbContext.Facilities.ToList(), "FacilityId", "Name");
        }

        public IActionResult Payment(string amount)
        {
            try
            {
                var currency = "zar";

                var successUrl = Url.Action("Success", "Booking", null, Request.Scheme);
                var cancelUrl = Url.Action("Cancel", "Booking", null, Request.Scheme);

                StripeConfiguration.ApiKey = _Settings.SecretKey;

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                Currency = currency,
                                UnitAmount = (long)(100 * 90),
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = "Booking amount Due"
                                }
                            },
                            Quantity = 1
                        }
                    },
                    Mode = "payment",
                    SuccessUrl = successUrl,
                    CancelUrl = cancelUrl
                };

                var service = new SessionService();
                var session = service.Create(options);

                return Redirect(session.Url);
            }
            catch
            {
                return Redirect("/Home/Index");
            }
        }

        public IActionResult Success()
        {
            return View("Success");
        }

        public IActionResult CancelPayment()
        {
            return View("Index");
        }

        [HttpGet]
        public IActionResult FeedBack()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FeedBack(FeedBack feedback)
        {
            if (ModelState.IsValid)
            {
                wrapper.Review.Add(feedback);
                wrapper.Save();

                return RedirectToAction("Index", "Booking");
            }

            return View(feedback);
        }
    }
}
