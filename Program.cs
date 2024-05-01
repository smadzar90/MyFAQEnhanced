using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyFAQEnhanced.Areas.Identity.Data;
using MyFAQEnhanced.Models;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DBContextIdentityConnection") ?? throw new InvalidOperationException("Connection string 'DBContextIdentityConnection' not found.");

builder.Services.AddDbContext<DBContextIdentity>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<DBContextIdentity>();

builder.Services.AddDbContext<FaqContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Add services to the container.
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=FAQ}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
