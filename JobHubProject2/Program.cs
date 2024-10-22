using JobHubProject2.Controllers;
using JobHubProject2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<JobHubDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("JobHubDbContext"));
});
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<JobHubDbContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/LogOut";
    options.AccessDeniedPath = "/Account/Notfound";
    options.ExpireTimeSpan = TimeSpan.FromDays(10);
    options.SlidingExpiration = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.HttpOnly = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    app.UseStatusCodePages(async context =>
    {
        var response = context.HttpContext.Response;
        if (response.StatusCode == 405)
        {
            response.Redirect("/Account/Notfound");
        }
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Main}/{action=HomePage}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(

        name : "MyAccountRoute",
        pattern:"Account/myAcc",
        defaults : new { controller = "Account", action = "MyAccount"});

    endpoints.MapControllerRoute(
     name: "JobsRoute",
     pattern: "Main/Jobs/{id?}",
     defaults: new { controller = "Main", action = "Jobs" });
});




app.Run();
