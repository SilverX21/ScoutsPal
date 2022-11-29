using ScoutsPal.Web;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Client Factory Configuration 
StaticDetails.ScoutManagementAPIBase = builder.Configuration["ServiceUrls:ScoutManagementAPI"];


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";

}).AddCookie("Cookies", c =>
{
    c.ExpireTimeSpan = TimeSpan.FromMinutes(1);
})
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = builder.Configuration["ServiceUrls:IdentityAPI"]; //URL for Identity Server
                options.GetClaimsFromUserInfoEndpoint = true;
                options.ClientId = "scout"; // Define in the Static Details of IdentityServer API
                options.ClientSecret = "secret";
                options.ResponseType = "code";


                options.TokenValidationParameters.NameClaimType = "name";
                options.TokenValidationParameters.RoleClaimType = "role";
                options.Scope.Add("scout");
                options.SaveTokens = true;

            });

#region Logs

string logsPath = "C:\\Logs_ScoutsPal";
DirectoryInfo di = Directory.CreateDirectory(logsPath); //creates logs folder

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File($"{logsPath}\\ScoutsPal_logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
Log.Information("ScoutsPalWeb App: started!");
#endregion

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
