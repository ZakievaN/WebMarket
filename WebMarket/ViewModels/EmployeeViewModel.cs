using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebMarket.ViewModels
{
    public class EmployeeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        [StringLength(200)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Имя")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Отчество")]
        [StringLength(200)]
        public string Patronymic { get; set; }

        [Required]
        [Display(Name = "Возраст")]
        [Range(18, 80)]
        public int Age { get; set; }
    }
}