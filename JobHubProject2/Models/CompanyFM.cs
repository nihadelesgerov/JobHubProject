using System.ComponentModel.DataAnnotations;

namespace JobHubProject2.Models
{
    public class CompanyFM
    {

        //DETAILED IDENTITY INFO FOR JOB SEEKERS
        [Required(ErrorMessage = "Company Name is required.")]
        [StringLength(100, ErrorMessage = "Company Name cannot exceed 100 characters.")]
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Industry is required.")]
        [StringLength(50, ErrorMessage = "Industry cannot exceed 50 characters.")]
        public string Industry { get; set; } = string.Empty;

        [Required(ErrorMessage = "Founded Year is required.")]
        [Range(1800, 2024, ErrorMessage = "Founded Year must be between 1800 and 2024.")]
        public string CompanyFoundedYear { get; set; } = string.Empty;

        [Required(ErrorMessage = "Company Type is required.")]
        [StringLength(50, ErrorMessage = "Company Type cannot exceed 50 characters.")]
        public string CompanyType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Company Size is required.")]
        public string CompanySize { get; set; } = string.Empty;

        [Required(ErrorMessage = "Head Office is required.")]
        public string CompanyHeadOffice { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string CompanyPhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string CompanyEmail { get; set; } = string.Empty;

        [Url(ErrorMessage = "Invalid URL format.")]
        public string CompanyWebsite { get; set; } = string.Empty;

        // INFO ABOUT COMPANY ITSELF
        [Required(ErrorMessage = "Overview is required.")]
        [StringLength(1000, ErrorMessage = "Overview cannot exceed 1000 characters.")]
        public string CompanyOverview { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Mission cannot exceed 500 characters.")]
        public string CompanyMission { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Vision cannot exceed 500 characters.")]
        public string CompanyVision { get; set; } = string.Empty;

        // SOCIAL MEDIA LINKS
        [Url(ErrorMessage = "Invalid LinkedIn URL format.")]
        public string CompanyLinkedInUrl { get; set; } = string.Empty;

        [Url(ErrorMessage = "Invalid Twitter URL format.")]
        public string CompanyTwitterUrl { get; set; } = string.Empty;

        [Url(ErrorMessage = "Invalid Facebook URL format.")]
        public string CompanyFacebookUrl { get; set; } = string.Empty;

        [Url(ErrorMessage = "Invalid Instagram URL format.")]
        public string CompanyInstagramUrl { get; set; } = string.Empty;
        //SETTINGS TO MANAGE CURRENT COMPANY

        //SETTINGS TO MANAGE CURRENT COMPANY
        public ICollection<Job> JobsList { get; set; }
    }
}
