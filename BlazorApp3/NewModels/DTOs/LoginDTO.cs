using System.ComponentModel.DataAnnotations;

namespace BlazerApp1.Models.DTOs
{
    public class LoginDTO
    {
        public int Id { get; set; }
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please enter {0}")]
        public string Username { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter {0}")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
