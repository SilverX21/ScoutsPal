using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Client Factory Configuration 
//builder.Services.AddHttpClient<IProductService, ProductService>();
//SD.ProductAPIBase = builder.Configuration["ServiceUrls:ScoutManagementAPI"];

string logsPath = "C:\\Logs_ScoutsPal";
DirectoryInfo di = Directory.CreateDirectory(logsPath); //creates logs folder

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File($"{logsPath}\\ScoutsPal_logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
Log.Information("ScoutsPalWeb App: started!");

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
