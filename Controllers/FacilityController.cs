using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UfsConnectBook.Data;
using UfsConnectBook.Models.Entities;

namespace UfsConnectBook.Controllers
{
    public class FacilityController : Controller
    {
        private readonly AppDbContext appDbContext;
        private readonly UserManager<AppUser> userManager;

        public FacilityController(AppDbContext appDbContext, UserManager<AppUser>
            userManager)
        {
            this.appDbContext = appDbContext;
            this.userManager = userManager;

        }
        public IActionResult Index()
        {
           

            var facilities = appDbContext.Facilities.ToList();
            for (int i = 0; i < facilities.Count(); i++)
            {
                facilities[i].Category = appDbContext.Category.Find(facilities[i].CategoryId);
            }
            return View(facilities);
        }
        public IActionResult Details(int FacilityId)
        {

            var facility = appDbContext.Facilities.Find(FacilityId);
            facility!.Category = appDbContext.Category.Find(facility.CategoryId);
            if (facility.Category.Name == "Gym")
            {
                facility.ImagePath = "/images/Gym.jpg";
            }
            else if (facility.Category.Name == "Laundry")
            {
                facility.ImagePath = "/images/LaundryR.jpg";
            }
            else if (facility.Category.Name == "Parking")
            {
                facility.ImagePath = "/images/ParkingSlots.jpg";
            }
            else if (facility.Category.Name == "Study")
            {
                facility.ImagePath = "/images/LibraryRoom.jpg";
            }

            else
            {
                facility.ImagePath = "/images/UfsMainPage1.jpg"; 
            }
            return View(facility);
        }
        public IActionResult Create()
        {
            
            PopulateDDL();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Facility facility)
        {
            
            if (!ModelState.IsValid)
            {
                PopulateDDL();
                return View(facility);
            }
            appDbContext.Facilities.Add(facility);
            appDbContext.SaveChanges();

            return RedirectToAction(nameof(Index),
                new { Message = $"New facility for has been created successful" });

        }
        public IActionResult Edit(int facilityId)
        {
            
            var facility = appDbContext.Facilities.Find(facilityId)!;
            facility.Category = appDbContext.Category.Find(facility.CategoryId);
            PopulateDDL();
            return View(facility);
        }
        [HttpPost]
        public IActionResult Edit(Facility facility)
        {
            
            if (!ModelState.IsValid)
            {
                PopulateDDL();
                return View(facility);
            }
            appDbContext.Facilities.Update(facility);
            appDbContext.SaveChanges();

            return RedirectToAction(nameof(Index),
                new { Message = $"Facility Updated Successful.." });

        }
        public IActionResult Delete(int facilityId)
        {
            
            var facility = appDbContext.Facilities.Find(facilityId)!;
            facility.Category = appDbContext.Category.Find(facility.CategoryId);
            return View(facility);
        }
        [HttpPost]
        public IActionResult Delete(Facility facility, int facilityId)
        {
            
            var _facility = appDbContext.Facilities.Find(facilityId)!;
            appDbContext.Facilities.Remove(_facility);
            appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index),
            new { Message = $"Facility was deleted Successful.." });
        }
        private void PopulateDDL()
        {
            ViewBag.CategoryId = new SelectList(appDbContext.Category.ToList(), "Id", "Name");
        }
    }
}
