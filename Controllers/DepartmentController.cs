using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly DbHelper db = new DbHelper();

        public IActionResult Index()
        {
            try
            {
                var departments = db.GetDepartments();

                return View(departments);
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Department dept)
        {
            try
            {
                db.AddDepartment(dept);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var dept = db.GetDepartmentById(id);

                return View(dept);
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }

        [HttpPost]
        public IActionResult Edit(Department dept)
        {
            try
            {
                db.UpdateDepartment(dept);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                db.DeleteDepartment(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }
    }
}