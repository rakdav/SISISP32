using Microsoft.AspNetCore.Mvc;
using PartyInvites.Models;
using System.Diagnostics;

namespace PartyInvites.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ViewResult RSVPForm()
        {
            return View();
        }
        [HttpPost]
        public ViewResult RSVPForm(GuestResponse guestResponse)
        {
            Repository.AddResponse(guestResponse);
            return View("Thanks",guestResponse);
        }
        public ViewResult ListResponces()
        {
            return View(Repository.Responses.Where(p =>p.WillAttend==true));
        }
    }
}
