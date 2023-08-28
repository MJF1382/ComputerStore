using ComputerStore.Database;
using ComputerStore.Database.Entities;
using ComputerStore.Database.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ComputerStoreDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services
    .AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<ComputerStoreDbContext>()
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
