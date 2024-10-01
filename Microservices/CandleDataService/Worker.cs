using CandleDataService.Models.Dtos;
using System.Text.Json;
using System.Text;
using CandleDataService.Models;
using CandleDataService.Repositories;
using CandleDataService.Services;
using CandleDataService.Data;

namespace CandleDataService
{
    //public class Worker : BackgroundService
    //{
    //    private readonly ILogger<Worker> _logger;
    //    private readonly IModel _channel;
    //    private readonly Random _random = new Random();

    //    public Worker(ILogger<Worker> logger, IConnection connection)
    //    {
    //        _logger = logger;
    //        _channel = connection.CreateModel();
    //        _channel.QueueDeclare(queue: "candles", durable: false, exclusive: false, autoDelete: false, arguments: null);
    //    }

    //    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    //    {
    //        //while (!stoppingToken.IsCancellationRequested)
    //        //{
    //        //    if (_logger.IsEnabled(LogLevel.Information))
    //        //    {
    //        //        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
    //        //    }
    //        //    await Task.Delay(1000, stoppingToken);
    //        //}


    //        while (!stoppingToken.IsCancellationRequested)
    //        {
    //            var random = new Random();
    //            var candleDto = new CandleDto
    //            {
    //                Open = random.Next(100, 200),
    //                Close = random.Next(100, 200),
    //                High = random.Next(200, 300),
    //                Low = random.Next(50, 100)
    //            };
    //            await _candleService.RegisterCandle(candleDto);

    //            //
    //            var candle = new Candle
    //            {
    //                Open = (decimal)_random.NextDouble() * 100,
    //                Close = (decimal)_random.NextDouble() * 100,
    //                High = (decimal)_random.NextDouble() * 100,
    //                Low = (decimal)_random.NextDouble() * 100
    //            };

    //            var message = JsonSerializer.Serialize(candle);
    //            var body = Encoding.UTF8.GetBytes(message);

    //            _channel.BasicPublish(exchange: "", routingKey: "candles", basicProperties: null, body: body);
    //            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
    //        }
    //    }
    //}
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly DotNetCore.CAP.ICapPublisher _capPublisher;
        private ApplicationDbContext _dbContext;
        private readonly IServiceProvider _serviceProvider;
        //private readonly ICandleService _candleService;
        // private readonly IServiceScopeFactory _scopeFactory;
        public Worker(ILogger<Worker> logger, DotNetCore.CAP.ICapPublisher capPublisher, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _capPublisher = capPublisher;
            _serviceProvider = serviceProvider;
            //_dbContext = dbContext;
            //_candleService = candleService;
        }
     

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
          
            while (!stoppingToken.IsCancellationRequested)
            {
            
                // if DataContext were configured as singleton through AddSingleton<DataContext> 
                // in ConfigureServices, this would always be the same instance. Since it's added 
                // as Scoped, we'll get the same instance every time we ask for the service from 
                // our scope but each time we create a new scope it'll be a new instance.
                using var scope = _serviceProvider.CreateScope();
                var services = scope.ServiceProvider;
                _dbContext = services.GetService<ApplicationDbContext>();
                if (_dbContext == null)
                {
                    throw new InvalidOperationException("Failed to retrieve ApplicationDbContext.");
                }              
                var random = new Random();
                var candleDto = new CandleDto
                {
                    Open = random.Next(100, 200),
                    Close = random.Next(100, 200),
                    High = random.Next(200, 300),
                    Low = random.Next(50, 100)
                };

                //save into sql database
                var savedCandle = await this.RegisterCandle(candleDto);

                //save into rabbit mq queue
                var content = System.Text.Json.JsonSerializer.Serialize<Candle>(savedCandle);
                //var jsonModel = JsonNode.Parse(content);
                var queueName = "CandleNewAdded";
                await _capPublisher.PublishAsync<string>(queueName, content);


                //var candle = new Candle
                //{
                //    Open = (decimal)_random.NextDouble() * 100,
                //    Close = (decimal)_random.NextDouble() * 100,
                //    High = (decimal)_random.NextDouble() * 100,
                //    Low = (decimal)_random.NextDouble() * 100
                //};
                //var message = JsonSerializer.Serialize(candle);
                //var body = Encoding.UTF8.GetBytes(message);
                //_channel.BasicPublish(exchange: "", routingKey: "candles", basicProperties: null, body: body);

                await Task.Delay(TimeSpan.FromSeconds(45), stoppingToken);

                _dbContext.Dispose();


            
                //}
            } 
        }

        public async Task<Candle> RegisterCandle(CandleDto candleDto)
        {
            var candle = new Candle
            {
                High = candleDto.High,
                Low = candleDto.Low,
                Open = candleDto.Open,
                Close = candleDto.Close
            };
            var savedCandle = await this.CreateCandle(candle);
            if (savedCandle != null)
            {
                var candleId = savedCandle.Id;

                //LOOOOOOGGGGGIIIIIIIIIIIIINNNNNNNNNNNNGGGGGGGGGGGGGGGGGGGGGGG using serilog into database
                // The creation was successful
                // Do something here (e.g., log success, notify user, etc.)
                return savedCandle;
            }
            else
            {
                //LOOOOOOGGGGGIIIIIIIIIIIIINNNNNNNNNNNNGGGGGGGGGGGGGGGGGGGGGGG using serilog into database
                // The creation failed
                // Handle the failure case (e.g., log error, throw exception, etc.)
                return null;
            }
        }

        public async Task<Candle> CreateCandle(Candle candle)
        {
            try
            {
                await _dbContext.candles.AddAsync(candle);
                await _dbContext.SaveChangesAsync();
                return candle;
            }
            catch (Exception)
            {

                return null;
            }

        }

    }

}
