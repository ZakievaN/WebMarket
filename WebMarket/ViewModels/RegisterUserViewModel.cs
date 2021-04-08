using System.ComponentModel.DataAnnotations;

namespace WebMarket.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required, MaxLength(256)]
        [Display(Name = "Логин")]
        public string UserName { get; init; }

        [Required, MaxLength(256)]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; init; }

        [Required, MaxLength(256)]
        [Display(Name = "Подтверждение пароля")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string PasswordConfirm { get; init; }
    }
}
