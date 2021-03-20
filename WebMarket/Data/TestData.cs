using System.Collections.Generic;
using WebMarket.Models;

namespace WebMarket.Data
{
    internal static class TestData
    {
        public static List<Employee> Employees = new()
        {
            new Employee { Id = 1, LastName = "Иванов", FirstName = "Иван", Patronymic = "Иванович", Age = 19 },
            new Employee { Id = 2, LastName = "Петров", FirstName = "Петр", Patronymic = "Петрович", Age = 25 },
            new Employee { Id = 3, LastName = "Сидоров", FirstName = "Сидор", Patronymic = "Сидорович", Age = 46 }
        };
    }
}
