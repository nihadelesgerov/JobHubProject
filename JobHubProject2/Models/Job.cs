using System.ComponentModel.DataAnnotations;

namespace JobHubProject2.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Skills {  get; set; } = string.Empty;
        public int Salary { get; set; }
        public string JobType { get; set; } = string.Empty;
        public DateTime PostedAt { get; set; } = DateTime.Now;
        public DateTime ApplicationDeadline { get; set; }

        // Foreign key for CompanyProfile
        public int CompanyProfileId { get; set; }
        public Company Company { get; set; }
    }
}
