using JobHubProject2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobHubProject2.Controllers
{
    [Authorize(Roles ="Admin")]
    [ValidateAntiForgeryToken]
    [AutoValidateAntiforgeryToken]
    public class Admin : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JobHubDbContext context;

        public Admin(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, JobHubDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.context = context;
        }
        public IActionResult AdminDashBoard()
        {
            ViewBag.TotalUsers = context.Users.Count();
            ViewBag.TotalJobs  = context.JobTable.Count();
            ViewBag.TotalEmployees = context.EmployeeTable.Count();
            ViewBag.TotalCompanies = context.CompanyTable.Count();

            return View();
        }
        public async Task<IActionResult> ManageUsers()
        {
            ViewBag.TotalUsers = await context.EmployeeTable.Include(a=>a.User).ToListAsync();

            ViewBag.TotalCompanies = await context.CompanyTable.ToListAsync();

            return View();
        }
        public async Task<IActionResult> ManageJobPostings()
        {

            ViewBag.Jobs = await context.JobTable.Include(a=>a.Company).ToListAsync();  
            return View();
        }
    }
}
