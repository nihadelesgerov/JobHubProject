using System.ComponentModel.DataAnnotations;

namespace JobHubProject2.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Please fill Email Adress")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please fill Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Compare("Password", ErrorMessage = "Passwords doesn't match")]
        public string ConfirmPassword { get; set; }
    }
}
