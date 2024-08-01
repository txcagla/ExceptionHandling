using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ExceptionHandling.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware pipeline configuration
if (!app.Environment.IsDevelopment())
{
    // Middleware for error pages
    app.UseExceptionHandler("/Error/Index"); // General error page
    app.UseStatusCodePages(); // Default handler for 404 page
}
else
{
    app.UseDeveloperExceptionPage(); // Detailed error page in development environment
}

// Custom middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
