using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebMarket.Models;

namespace WebMarket.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<Employee> _employees = new()
        {
            new Employee { Id = 1, LastName = "Иванов", FirstName = "Иван", Patronymic = "Иванович", Age = 19 },
            new Employee { Id = 2, LastName = "Петров", FirstName = "Петр", Patronymic = "Петрович", Age = 25 },
            new Employee { Id = 3, LastName = "Сидоров", FirstName = "Сидор", Patronymic = "Сидорович", Age = 46 }
        };

        private Employee GetEmployeeById(int id)
        {
            return _employees.Find(empployee => empployee.Id == id);
        }

        public IActionResult Index()
        {
            return View();
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
    }
}