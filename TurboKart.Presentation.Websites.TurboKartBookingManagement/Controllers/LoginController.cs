using Microsoft.AspNetCore.Mvc;
using TurboKart.Presentation.Websites.TurboKartBookingManagement.Models;

namespace TurboKart.Presentation.Websites.TurboKartBookingManagement.Controllers;

public class LoginController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    [ActionName("Index")]
    public IActionResult LoginVerify(LoginModel loginModel)
    {
        if (ModelState.IsValid)
        {
            if (loginModel.Username.ToLower() == "test" && loginModel.Password == "test")
            {
                return RedirectToAction("Index", "Booking");
            }
        }

        return View();
    }
}