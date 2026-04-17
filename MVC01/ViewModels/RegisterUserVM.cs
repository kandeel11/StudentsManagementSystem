using System.ComponentModel.DataAnnotations;

namespace MVC01.ViewModels
{
    public class RegisterUserVM
    {
        
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
         public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
         public string Email { get; set; }
        public string? ImageUrl { get; set; }

    }
}
