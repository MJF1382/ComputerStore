using ComputerStore.Classes;
using ComputerStore.Database;
using ComputerStore.Database.Entities;
using ComputerStore.Database.IdentityProviders;
using ComputerStore.Database.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options => options.Filters.Add(typeof(ApiResultFilterAttribute)));
builder.Services.AddDbContext<ComputerStoreDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services
    .AddIdentity<AppUser, AppRole>(options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<ComputerStoreDbContext>()
    .AddUserManager<AppUserManager>()
    .AddDefaultTokenProviders();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapFallbackToFile("index.html");

app.Run();
