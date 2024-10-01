using CandleMainService.Data;
using CandleMainService.SignalRHubs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
//builder.Services.AddSingleton<CandleHub>();
//builder.Services.AddSignalR();

//add cap implementation
builder.Services.AddCap(options =>
{
    options.UseEntityFramework<ApplicationDbContext>();
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

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


var app = builder.Build();


app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
   // endpoints.MapHub<CandleHub>("/candlehub");
});


app.Run();
