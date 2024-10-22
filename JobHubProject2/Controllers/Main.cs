using JobHubProject2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JobHubProject2.Controllers
{
    public class Main : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JobHubDbContext context;

        public Main(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, JobHubDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.context = context;
        }
        public async Task<IActionResult> HomePage()
        {
            var listOfJobs = await context.JobTable.Include(a=>a.Company).ToListAsync();
            return View(listOfJobs);
        }
        [Authorize]
        public async Task<IActionResult> CompanyAccount()
        {

            var company =  await userManager.GetUserAsync(User);
            if (company == null)
            {
                return RedirectToAction("Notfound");
            }
            ViewBag.Email = company.Email;    

            var currentCompany =  await context.CompanyTable.FirstOrDefaultAsync(a=>a.UserId == company.Id);
            if(currentCompany == null)
            {
                return NotFound();
            }
            ViewBag.CompanyName = currentCompany.CompanyName;
            ViewBag.CompanyFoundedYear = currentCompany.CompanyFoundedYear;
            ViewBag.CompanyType = currentCompany.CompanyType;
            ViewBag.CompanySize = currentCompany.CompanySize;
            ViewBag.CompanyHeadOffice = currentCompany.CompanyHeadOffice;
            ViewBag.CompanyPhone = currentCompany.CompanyPhone;
            ViewBag.CompanyEmail = currentCompany.CompanyEmail;
            ViewBag.CompanyWebsite = currentCompany.CompanyWebsite;
            ViewBag.CompanyOverview = currentCompany.CompanyOverview;
            ViewBag.CompanyMission = currentCompany.CompanyMission;
            ViewBag.CompanyLinkedInUrl = currentCompany.CompanyLinkedInUrl;
            ViewBag.CompanyTwitterUrl = currentCompany.CompanyTwitterUrl;
            ViewBag.CompanyFacebookUrl = currentCompany.CompanyFacebookUrl;
            ViewBag.CompanyInstagramUrl = currentCompany.CompanyInstagramUrl;

            var joblistofCurrentCompany = context.JobTable.Where(a=>a.CompanyProfileId==currentCompany.Id).ToList();

            return View(joblistofCurrentCompany);
        }
        [Authorize]
        public async Task<IActionResult> EditCompanyInfo()
        {

            var user = await userManager.GetUserAsync(User);
            if(user == null)
            {
                return RedirectToAction("Notfound");
            }
            ViewBag.Email = user.Email;
            var currentCompany = await context.CompanyTable.FirstOrDefaultAsync(a=>a.UserId==user.Id);

            if(currentCompany == null)
            {
                return RedirectToAction("Notfound");
            }
            ViewBag.CompanyName = currentCompany.CompanyName;
            ViewBag.CompanyFoundedYear = currentCompany.CompanyFoundedYear;
            ViewBag.CompanyType = currentCompany.CompanyType;
            ViewBag.CompanySize = currentCompany.CompanySize;
            ViewBag.CompanyHeadOffice = currentCompany.CompanyHeadOffice;
            ViewBag.CompanyPhone = currentCompany.CompanyPhone;
            ViewBag.CompanyEmail = currentCompany.CompanyEmail;
            ViewBag.CompanyWebsite = currentCompany.CompanyWebsite;
            ViewBag.CompanyOverview = currentCompany.CompanyOverview;
            ViewBag.CompanyMission = currentCompany.CompanyMission;
            ViewBag.CompanyLinkedInUrl = currentCompany.CompanyLinkedInUrl;
            ViewBag.CompanyTwitterUrl = currentCompany.CompanyTwitterUrl;
            ViewBag.CompanyFacebookUrl = currentCompany.CompanyFacebookUrl;
            ViewBag.CompanyInstagramUrl = currentCompany.CompanyInstagramUrl;


            return View();
        }
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditCompanyInfo(CompanyFM model)
        {
                var user = await userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Notfound");
                }

                var companyInfo = await context.CompanyTable.FirstOrDefaultAsync(a => a.UserId == user.Id);

                if (companyInfo == null)
                {
                    return RedirectToAction("Notfound");
                }

                companyInfo.CompanyName = model.CompanyName;
                companyInfo.CompanyFoundedYear = model.CompanyFoundedYear;
                companyInfo.CompanyType = model.CompanyType;
                companyInfo.CompanySize = model.CompanySize;
                companyInfo.CompanyHeadOffice = model.CompanyHeadOffice;
                companyInfo.CompanyPhone = model.CompanyPhone;
                companyInfo.CompanyEmail = model.CompanyEmail;
                companyInfo.CompanyWebsite = model.CompanyWebsite;
                companyInfo.CompanyOverview = model.CompanyOverview;
                companyInfo.CompanyMission = model.CompanyMission;
                companyInfo.CompanyLinkedInUrl = model.CompanyLinkedInUrl;
                companyInfo.CompanyTwitterUrl = model.CompanyTwitterUrl;
                companyInfo.CompanyFacebookUrl = model.CompanyFacebookUrl;
                companyInfo.CompanyInstagramUrl = model.CompanyInstagramUrl;

                context.CompanyTable.Update(companyInfo);
                await context.SaveChangesAsync();

                return RedirectToAction("CompanyAccount", "Main");
        }
        public IActionResult JobPost()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> JobPost(JobFM model)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Notfound");
            }
            var company = context.CompanyTable.FirstOrDefault(a =>a.UserId ==user.Id);
            if (company == null)
            {
                return RedirectToAction("Notfound");
            }
            var job = new Job
            {
                Title = model.Title,
                Description = model.Description,
                Skills = model.Skills,
                Salary = model.Salary,
                Location = model.Location,
                ApplicationDeadline = model.ApplicationDeadline,
                CompanyProfileId = company.Id
            };

            context.JobTable.Add(job);
            await context.SaveChangesAsync();

            return RedirectToAction("CompanyAccount", "Main");

        }
        [Route("Jobs/{id?}")]
        public IActionResult Jobs(int id)
        {
            var currentJob =   context.JobTable.Include(a=>a.Company).FirstOrDefault(a=>a.Id==id);
            if (currentJob == null)
            {
                return RedirectToAction("Notfound");
            }

                var list_of_other_jobs = context.JobTable.Include(a=>a.Company)./*Where(a=>a.Title==currentJob.Title).*/ToList(); // there's a few job exist rn

            ViewBag.CurrentJob  = currentJob;
            ViewBag.Title = currentJob.Title + currentJob.Location + currentJob.Company.CompanyName;
            return View(list_of_other_jobs);
        }
    }
}
