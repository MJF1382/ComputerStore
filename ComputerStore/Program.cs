using ComputerStore.Database;
using ComputerStore.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ComputerStoreDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services
    .AddIdentity<AppUser, IdentityRole<string>>()
    .AddEntityFrameworkStores<ComputerStoreDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
