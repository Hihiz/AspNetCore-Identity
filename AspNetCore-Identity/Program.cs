using Microsoft.EntityFrameworkCore;
using AspNetCore_Identity.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ApplicationContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationContextConnection' not found.");

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationIdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
