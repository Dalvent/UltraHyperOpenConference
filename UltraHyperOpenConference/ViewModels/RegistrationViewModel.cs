using System.ComponentModel.DataAnnotations;

namespace UltraHyperOpenConference.ViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
 
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
 
        [Required]
        [Compare("Password", ErrorMessage = "Passwords is not the same!")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}