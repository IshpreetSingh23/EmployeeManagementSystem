using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly DbHelper db = new DbHelper();

        // ================= View Attendance =================

        public IActionResult Index()
        {
            var attendance = db.GetAttendance();
            return View(attendance);
        }

        // ================= Add Attendance =================

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Attendance attendance)
        {
            bool result = db.AddAttendance(attendance);

            if (result)
            {
                TempData["Success"] = "Attendance Added Successfully!";
                return RedirectToAction("Index");
            }

            return View(attendance);
        }

        // ================= Edit Attendance =================

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Attendance? attendance = db.GetAttendanceById(id);

            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        [HttpPost]
        public IActionResult Edit(Attendance attendance)
        {
            bool result = db.UpdateAttendance(attendance);

            if (result)
            {
                TempData["Success"] = "Attendance Updated Successfully!";
                return RedirectToAction("Index");
            }

            return View(attendance);
        }

        // ================= Delete Attendance =================

        public IActionResult Delete(int id)
        {
            db.DeleteAttendance(id);

            TempData["Success"] = "Attendance Deleted Successfully!";

            return RedirectToAction("Index");
        }
    }
}