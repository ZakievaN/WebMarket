using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebMarket.Data;
using WebMarket.Infrastructure.Services.Interfaces;
using WebMarket.Models;
using WebMarket.ViewModels;

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

        public IActionResult Remove(int id)
        {
            bool isDelete = _employeesData.Delete(id);

            if (!isDelete)
            {
                return NotFound();
            }

            return RedirectToAction("Index", _employees);
        }

        public IActionResult Edit(int? id)
        {
            if (id is null)
                return View(new EmployeeViewModel());

            var employee = _employeesData.Get((int)id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(new EmployeeViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Patronymic = employee.Patronymic,
                Age = employee.Age
            });
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model)
        {
            var employee = new Employee
            {
                Id = model.Id,
                LastName = model.LastName,
                FirstName = model.FirstName,
                Patronymic = model.Patronymic,
                Age = model.Age
            };

            if (employee.Id == 0)
            {
                _employeesData.Add(employee);
            }
            else
            {
                _employeesData.Update(employee);    
            }

            _employeesData.Update(employee);

            return RedirectToAction("Index");
        }
    }
}
