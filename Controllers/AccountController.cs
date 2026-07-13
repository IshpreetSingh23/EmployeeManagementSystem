using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Data;

namespace EmployeeManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        DbHelper db = new DbHelper();

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = db.ValidateUser(email, password);

            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = "Invalid Email or Password";
            return View();
        }
    }
}