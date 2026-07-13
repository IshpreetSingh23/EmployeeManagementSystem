using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using System;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DbHelper db = new DbHelper();

        // ================= Employee List =================

        public IActionResult Index()
        {
            var employees = db.GetEmployees();
            return View(employees);
        }

        // ================= Add Employee =================

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Employee emp)
        {
            try
            {
                bool result = db.AddEmployee(emp);

                if (result)
                {
                    TempData["Success"] = "Employee Added Successfully!";
                    return RedirectToAction("Index");
                }

                ViewBag.Message = "Employee could not be added.";
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return View(emp);
        }

        // ================= Edit Employee =================

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee? emp = db.GetEmployeeById(id);

            if (emp == null)
            {
                return NotFound();
            }

            return View(emp);
        }

        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            bool result = db.UpdateEmployee(emp);

            if (result)
            {
                TempData["Success"] = "Employee Updated Successfully!";
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Employee could not be updated.";

            return View(emp);
        }

        // ================= Delete Employee =================

        public IActionResult Delete(int id)
        {
            bool result = db.DeleteEmployee(id);

            if (result)
            {
                TempData["Success"] = "Employee Deleted Successfully!";
            }
            else
            {
                TempData["Error"] = "Employee could not be deleted.";
            }

            return RedirectToAction("Index");
        }
    }
}