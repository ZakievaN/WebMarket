using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebMarket.Data;
using WebMarket.Infrastructure.Services.Interfaces;
using WebMarket.Models;

namespace WebMarket.Controllers
{
    public class EmployeesController : Controller
    {
        private IEmployeesData _employeesData;

        private readonly List<Employee> _employees;

        public EmployeesController(IEmployeesData employeesData)
        {
            _employeesData = employeesData;
        }

        public IActionResult Index()
        {
            return View(_employeesData.Get());
        }

        public IActionResult Employees()
        {
            return View(_employees);
        }

        public IActionResult Details(int id)
        {
            var employee = _employeesData.Get(id);
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

        public IActionResult Save(int id, Employee emp)
        {
            var employee = GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }

            employee.LastName = emp.LastName;
            employee.FirstName = emp.FirstName;
            employee.Patronymic = emp.Patronymic;
            employee.Age = emp.Age;

            return View("Details", employee);
        }

        public IActionResult Remove(int id)
        {
            var employee = GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }

            _employees.Remove(employee);

            return RedirectToAction("Index", _employees);
        }
    }
}
