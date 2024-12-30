using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Animal_Cafe_Core_Web_App.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<Animal_Cafe_Core_Web_AppContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Animal_Cafe_Core_Web_AppContext")
        ?? throw new InvalidOperationException("Connection string 'Animal_Cafe_Core_Web_AppContext' not found.")));

builder.Services.AddDbContext<Animal_CafeIdentityContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Animal_Cafe_Core_Web_AppContext")
        ?? throw new InvalidOperationException("Connection string 'Animal_Cafe_Core_Web_AppContext' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<Animal_CafeIdentityContext>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
