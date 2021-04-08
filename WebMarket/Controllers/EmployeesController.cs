using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMarket.Infrastructure.Services.Interfaces;
using WebMarket.Models;
using WebMarket.ViewModels;

namespace WebMarket.Controllers
{
    public class EmployeesController : Controller
    {
        private IEmployeesData _employeesData;

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
            return View(_employeesData.Get());
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

        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();

            var employee = _employeesData.Get(id);

            if (employee is null)
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

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _employeesData.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
            {
                return View(new EmployeeViewModel());
            }

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
        [Authorize(Roles = "admin")]
        public IActionResult Edit(EmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

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

            return RedirectToAction("Index");
        }
    }
}