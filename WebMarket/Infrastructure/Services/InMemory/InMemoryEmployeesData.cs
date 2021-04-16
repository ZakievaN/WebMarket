using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMarket.Data;
using WebMarket.Infrastructure.Services.Interfaces;
using WebMarket.Models;

namespace WebMarket.Infrastructure.Services.InMemory
{
    public class InMemoryEmployeesData : IEmployeesData
    {

        private readonly List<Employee> _employees;

        private int _currentMaxId;

        public InMemoryEmployeesData()
        {
            _employees = TestData.Employees;
            _currentMaxId = _employees.DefaultIfEmpty().Max(e => e?.Id ?? 1);
        }

        public int Add(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            if (_employees.Contains(employee)) return employee.Id; // в бд это не нужно  будет

            employee.Id = ++_currentMaxId;
            _employees.Add(employee);

            return employee.Id;
        }

        public Employee Add(string lastName, string firstName, string patronymic, int age)
        {
            var employee = new Employee
            {
                LastName = lastName,
                FirstName = firstName,
                Patronymic = patronymic,
                Age = age
            };
            Add(employee);
            return employee;
        }

        public bool Delete(int id)
        {
            var db_item = Get(id);

            if (db_item is null) return false;

            return _employees.Remove(db_item);
        }

        public IEnumerable<Employee> Get()
        {
            return _employees;
        }

        public Employee Get(int id)
        {
            return _employees.FirstOrDefault(e => e.Id == id);
        }

        public void Update(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            if (_employees.Contains(employee)) return; // в бд это не нужно будет

            var db_item = Get(employee.Id);

            if (db_item is null) return;

            db_item.LastName = employee.LastName;
            db_item.FirstName = employee.FirstName;
            db_item.Patronymic = employee.Patronymic;
            db_item.Age = employee.Age;
        }
    }
}
