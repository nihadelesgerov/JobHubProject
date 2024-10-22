using System.ComponentModel.DataAnnotations;

namespace JobHubProject2.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Please fill Email")]
        [EmailAddress(ErrorMessage ="Email type must be like this johndoe@domain.com")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please fill Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
