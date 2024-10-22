using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobHubProject2.Models
{
    public class JobHubDbContext : IdentityDbContext<AppUser>
    {
        public JobHubDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Company> CompanyTable { get; set; }
        public DbSet<Employee > EmployeeTable { get; set; }
        public DbSet<Job> JobTable { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //relationship between user and employee
            builder.Entity<AppUser>().HasOne(a=>a.EmployeeProfile).WithOne(e=>e.User).HasForeignKey<Employee>(c=>c.UserId).OnDelete(DeleteBehavior.Cascade);

            //relation between user and company
            builder.Entity<AppUser>().HasOne(a => a.CompanyProfile).WithOne(u => u.User).HasForeignKey<Company>(c => c.UserId).OnDelete(DeleteBehavior.Cascade);

            //relationship between copany and joblist (job) (one to many)
            builder.Entity<Company>().HasMany(a=>a.JobsList).WithOne(c=>c.Company).HasForeignKey(f=>f.CompanyProfileId).OnDelete(DeleteBehavior.Cascade);  


        }
    }
}
