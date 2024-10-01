using CandleDataService;
using CandleDataService.Data;
using CandleDataService.Repositories;
using CandleDataService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);

//using serilog
//var webbuilder = WebApplication.CreateBuilder(args);
//webbuilder.Host.UseSerilog((context, services, configuration) => configuration
//                .ReadFrom.Configuration(context.Configuration));

// Add Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddScoped<ICandleRepository, CandleRepository>();
//builder.Services.AddScoped<ICandleService, CandleService>();
builder.Services.AddHostedService<Worker>();



#region Add Cap library For Rabbit
builder.Services.AddCap(options =>
{
    options.UseEntityFramework<ApplicationDbContext>();
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseDashboard(path => path.PathMatch = "/cap-dashboard");
    options.UseRabbitMQ(options =>
    {
        options.ConnectionFactoryOptions = options =>
        {
            options.Ssl.Enabled = false;
            options.HostName = "localhost";
            options.UserName = "guest";
            options.Password = "guest";
            options.Port = 5672;
        };
    });
});
#endregion


var host = builder.Build();
host.Run();
