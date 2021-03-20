using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMarket.Data;
using WebMarket.Models;

namespace WebMarket.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly List<Employee> _employees;

        public EmployeesController()
        {
            _employees = TestData.Employees;
        }

        public IActionResult Index()
        {
            return View(_employees);
        }

        public IActionResult Employees()
        {
            return View(_employees);
        }

        public IActionResult Details(int id)
        {
            var employee = GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        private Employee GetEmployeeById(int id)
        {
            return _employees.FirstOrDefault(empployee => empployee.Id == id);
        }
    }
}
