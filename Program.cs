using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Rumo.Data;
using Rumo.Data.Configure;
using Rumo.Data.Repository.VehicleRepository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(o =>
o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddConfigureData(builder.Configuration);

builder.Services.AddMemoryCache();

var cacheEntryOptions = new MemoryCacheEntryOptions()
    .SetSlidingExpiration(TimeSpan.FromMinutes(5))  
    .SetAbsoluteExpiration(TimeSpan.FromHours(1));  

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
