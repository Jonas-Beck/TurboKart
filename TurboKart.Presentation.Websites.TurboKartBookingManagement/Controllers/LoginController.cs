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
        // Validate loginModel 
        if (ModelState.IsValid)
        {
            // TODO Add Microsoft Authentication
            
            // Validate Username and Password == "test"
            if (loginModel.Username.ToLower() == "test" && loginModel.Password == "test")
            {
                // Redirect to Booking Controllers Index
                return RedirectToAction("Index", "Booking");
            }
        }

        // Return login view 
        return View();
    }
}