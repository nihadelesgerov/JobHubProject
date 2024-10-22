using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobHubProject2.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        //NAVIGATION PROPERTIES
        public string UserId { get; set; }
        public AppUser User { get; set; }

        //Detailed Info About User
        public string Skills { get; set; } = string.Empty;
        public string Education { get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
        public string SocialMediaLinks { get; set; } = string.Empty;
        public DateTime AccountCreatedAt { get; set; } = DateTime.Now;
        public string CvImagePath { get; set; } = string.Empty;

        [NotMapped]
        public IFormFile CvFile { get; set; }
    }
}
