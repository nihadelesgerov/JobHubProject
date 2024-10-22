using Microsoft.AspNetCore.Identity;

namespace JobHubProject2.Models
{
    public class AppUser : IdentityUser
    {
        //navigation properties
        public Company CompanyProfile { get; set; }
        public Employee EmployeeProfile { get; set; }
    }
}
