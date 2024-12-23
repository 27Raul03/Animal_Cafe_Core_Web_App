using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Animal_Cafe_Core_Web_App.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<Animal_Cafe_Core_Web_AppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Animal_Cafe_Core_Web_AppContext") ?? throw new InvalidOperationException("Connection string 'Animal_Cafe_Core_Web_AppContext' not found.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
