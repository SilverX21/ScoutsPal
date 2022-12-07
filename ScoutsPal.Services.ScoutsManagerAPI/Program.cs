using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using ScoutsPAl.Services.ScoutsManagerAPI.DbContexts;
using ScoutsPAl.Services.ScoutsManagerAPI.Helpers;
using ScoutsPAl.Services.ScoutsManagerAPI.Services;
using ScoutsPAl.Services.ScoutsManagerAPI.Services.Interfaces;
using Serilog;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//DataBase connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    ScoutsPalConfigurationHelper.AddSwaggerDocumentation(c);
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

//DI
builder.Services.AddScoped<IScoutRepository, ScoutRepository>();
builder.Services.AddScoped<IScoutTypeRepository, ScoutTypeRepository>();

string logsPath = "C:\\Logs_ScoutsPal";
DirectoryInfo di = Directory.CreateDirectory(logsPath); //creates logs folder

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File($"{logsPath}\\ScoutsPal_logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
Log.Information("ScoutsPal App: started!");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();