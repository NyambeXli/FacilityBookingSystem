using Microsoft.AspNetCore.Mvc;
using UfsConnectBook.Models.Entities;

namespace UfsConnectBook.Controllers
{
    public class ReviewController : Controller
    {

        public IActionResult HandleFeedback()
        {
           
            return View();
        }
    }
}
