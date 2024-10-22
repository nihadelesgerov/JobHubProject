using JobHubProject2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace JobHubProject2.Controllers
{
    public class Account : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JobHubDbContext context;

        public Account(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, JobHubDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.context = context;
        }
        public IActionResult Notfound()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, true, true);
                if (result.Succeeded)
                {
                    return RedirectToAction("HomePage", "Main");
                }
                ModelState.AddModelError("LoginFailed", "Invalid Email or Password");
                return View(model);
            }
            return View(model);
        }
        public IActionResult RegisterEmployee()
        {
            return View();
        }
        public IActionResult RegisterCompany()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> RegisterCompany(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await userManager.CreateAsync(user, model.ConfirmPassword);
                if (result.Succeeded)
                {
                    if (!await roleManager.RoleExistsAsync("Company"))
                    {
                        await roleManager.CreateAsync(new IdentityRole ("Company"));
                        await userManager.AddToRoleAsync(user,"Company");
                    }

                    var company = new Company
                    {
                        User = user,
                        UserId = user.Id,
                    };
                    await context.CompanyTable.AddAsync(company);
                    await context.SaveChangesAsync();
                    await userManager.AddToRoleAsync(user, "Company");
                    await signInManager.SignInAsync(user, isPersistent: true, null);
                    return RedirectToAction("HomePage", "Main");
                }
                ModelState.AddModelError("registerFailed", "Registration process failed, please try again later");
                return View(model);
            }
            return View(model);
        }


        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> RegisterEmployee(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await userManager.CreateAsync(user, model.ConfirmPassword);
                if (result.Succeeded)
                {
                    if (!await roleManager.RoleExistsAsync("Employee"))
                    {
                        await roleManager.CreateAsync(new IdentityRole("Employee"));
                        await userManager.AddToRoleAsync(user, "Employee");
                    }
                    var employee = new Employee
                    {
                        User = user,
                        UserId = user.Id,
                    };
                    await context.EmployeeTable.AddAsync(employee);
                    await context.SaveChangesAsync();
                    await userManager.AddToRoleAsync(user, "Employee");
                    await signInManager.SignInAsync(user, isPersistent: true, null);
                    return RedirectToAction("HomePage", "Main");
                }
            }
            return View(model);
        }
        [Authorize]
        [Route("myAcc")]
        public async Task<IActionResult> MyAccount()
        {

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Notfound");
            }
            ViewBag.Email = user.Email;

            var userDetails = context.EmployeeTable.FirstOrDefault(a => a.UserId == user.Id);

            if (userDetails == null)
            {
                return RedirectToAction("Notfound");
            }

            ViewBag.Skills = userDetails.Skills;
            ViewBag.Education = userDetails.Education;
            ViewBag.Experience = userDetails.Experience;
            ViewBag.Industry = userDetails.Industry;
            ViewBag.SocialMediaLinks = userDetails.SocialMediaLinks;
            ViewBag.CvImagePath = userDetails.CvImagePath;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("HomePage", "Main");
        }
        [Authorize]
        public async Task<IActionResult> EditInfo()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Notfound");
            }
            var userDetails = context.EmployeeTable.FirstOrDefault(a => a.UserId == user.Id);

            if (userDetails == null)
            {
                return RedirectToAction("Notfound");
            }
            ViewBag.Email = user.Email;
            ViewBag.Skills = userDetails.Skills;
            ViewBag.Education = userDetails.Education;
            ViewBag.Experience = userDetails.Experience;
            ViewBag.Industry = userDetails.Industry;
            ViewBag.SocialMediaLinks = userDetails.SocialMediaLinks;
            return View();
        }

        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditInfo(EmployeeFM model)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Notfound");
            }
            var employeeInfo = await context.EmployeeTable
           .FirstOrDefaultAsync(e => e.UserId == user.Id);

            if(employeeInfo == null)
            {
                return RedirectToAction("Notfound");
            }

            employeeInfo.Skills = model.Skills;
                employeeInfo.Education = model.Education;
                employeeInfo.Experience = model.Experience;
                employeeInfo.Industry = model.Industry;
                employeeInfo.SocialMediaLinks = model.SocialMediaLinks;

                context.EmployeeTable.Update(employeeInfo);
                context.SaveChanges();
                return RedirectToAction("HomePage", "Main");
        }
    }
}