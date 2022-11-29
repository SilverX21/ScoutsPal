using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScoutsPal.Services.ScoutsIdentityManagerAPI;
using ScoutsPal.Services.ScoutsIdentityManagerAPI.DbContexts;
using ScoutsPal.Services.ScoutsIdentityManagerAPI.Initializer;
using ScoutsPal.Services.ScoutsIdentityManagerAPI.Models;
using ScoutsPal.Services.ScoutsIdentityManagerAPI.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

#region Defines database Connection String
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion
// Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddIdentityServer(options =>
{

    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;
    
}).AddInMemoryIdentityResources(StaticDetails.IdentityResources)
.AddInMemoryApiScopes(StaticDetails.ApiScopes)
.AddInMemoryClients(StaticDetails.Clients)
.AddAspNetIdentity<ApplicationUser>() 
.AddDeveloperSigningCredential();
builder.Services.AddAuthentication().AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
  
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
});

builder.Services.Configure<IdentityOptions>(options =>
{
    
    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;
});

builder.Services.AddScoped<IDbInitializer, DbInitializer>();

//Inset Type of profile on access Token
builder.Services.AddScoped<IProfileService, ProfileService>();
// Add services to the container.
builder.Services.AddControllersWithViews();

string logsPath = "C:\\Logs_ScoutsPal";
DirectoryInfo di = Directory.CreateDirectory(logsPath); //creates logs folder

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File($"{logsPath}\\ScoutsPal_logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
Log.Information("ScoutsPalIdentity App: started!");

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
app.UseIdentityServer();
app.UseAuthorization();
SeedDatabase();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

//Populates database on first time run program
void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}