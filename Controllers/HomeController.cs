using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Data;

namespace EmployeeManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        DbHelper db = new DbHelper();

        public IActionResult Index()
        {
            ViewBag.EmployeeCount = db.GetEmployeeCount();
            ViewBag.DepartmentCount = db.GetDepartmentCount();
            ViewBag.PresentCount = db.GetPresentCount();
            ViewBag.AbsentCount = db.GetAbsentCount();

            return View();
        }
    }
}