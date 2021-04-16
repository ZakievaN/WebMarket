using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebMarket.ViewModels
{
    public class LoginViewModel
    {
        [Required, MaxLength(256)]
        [Display(Name = "Логин")]
        public string UserName { get; init; }

        [Required, MaxLength(256)]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; init; }

        [Display(Name = "Запомнить")]
        public bool IsRemember { get; init; }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; init; }
    }
}
