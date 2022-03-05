using ECommerceSite.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. Configures automatically passing crateContext into controller
// when asked by a constructor. Specifies to use sqlServer with specified connection string
// --dependency injection--
builder.Services.AddDbContext<CrateContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();

//When using HttpContextAccessor, create an instance of that class
//on views so the data can be accessed. For custom interfaces
//builder.Services.AddSingleton<HttpContextAccessor, HttpContextAccessor>();

//MS' auto setup of that
builder.Services.AddHttpContextAccessor();


// adds ability to use sessions. Must also inlcude UseSession below. Documentation:
// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-6.0#session-state
builder.Services.AddDistributedMemoryCache();
//options only if needed, default 15 mins
builder.Services.AddSession(options =>
    options.IdleTimeout = TimeSpan.FromMinutes(10));

//must add services before building
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
//activate ability to use sessions 
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
