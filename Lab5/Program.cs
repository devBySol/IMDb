using Lab5.Data;  
using Lab5.Data.Models;  
using Lab5.Data.Repositories;  
using Lab5.Services;  
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddSingleton<IMovieRepository, MockMovieRepository>();
builder.Services.AddScoped<IMovieRepository, SQLMovieRepository>();

builder.Services.AddSingleton<MovieService>();
builder.Services.AddHttpClient();

var connectionString = builder.Configuration.GetConnectionString("MovieDBConnection");

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseMySql(connectionString,
        ServerVersion.AutoDetect(connectionString),
        b => b.MigrationsAssembly("Lab5.Data"))
);



Console.WriteLine($"💛Running Environment: {builder.Environment.EnvironmentName}💛");

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<AppDBContext>()
.AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
} else
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
