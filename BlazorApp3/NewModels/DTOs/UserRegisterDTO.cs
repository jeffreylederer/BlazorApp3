using System.ComponentModel.DataAnnotations;

namespace BlazerApp1.Models.DTOs
{
    public class UserRegisterDTO
    {

        public int Id { get; set; }
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please enter {0}")]
        public string Username { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter {0}")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Rewrite the password")]
        [Required(ErrorMessage = "Please enter {0}")]
        [Compare("Password", ErrorMessage = "{0} and {1} are not the same.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

    }
}
