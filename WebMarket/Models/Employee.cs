using System;

namespace WebMarket.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public int Age { get; set; }

        public int BirthYear 
        {
            get
            {
                return DateTime.Now.Subtract(new TimeSpan(Age > 0 ? Age * 365: 0, 0, 0, 0)).Year;
            } 
        }
    }
}
