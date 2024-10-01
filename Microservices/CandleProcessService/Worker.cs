namespace CandleProcessService
{
    //public class Worker : BackgroundService
    //{
    //    private const string UserName = "guest";
    //    private const string Password = "guest";
    //    private const string HostName = "localhost";

    //    private readonly ILogger<Worker> _logger;
    //    //private readonly IModel _channel;
    //    // private readonly ApplicationDbContext _context;
    //    private IConnection _connection;
    //    private IModel _channel;

    //    public Worker(ILogger<Worker> logger, IConnection connection/*, ApplicationDbContext context*/)
    //    {
    //        _logger = logger;
    //        //this._logger = loggerFactory.CreateLogger<ConsumeRabbitMQHostedService>();
    //        InitRabbitMQ();
    //    }
    //    private void InitRabbitMQ()
    //    {
    //        var factory = new ConnectionFactory { HostName = "localhost" };

    //        // create connection
    //        _connection = factory.CreateConnection();

    //        // create channel
    //        _channel = _connection.CreateModel();

    //        _channel.ExchangeDeclare("demo.exchange", ExchangeType.Topic);
    //        _channel.QueueDeclare("demo.queue.log", false, false, false, null);
    //        _channel.QueueBind("demo.queue.log", "demo.exchange", "demo.queue.*", null);
    //        _channel.BasicQos(0, 1, false);

    //        //  _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
    //    }
    //    private void HandleMessage(string content)
    //    {
    //        // we just print this message
    //        _logger.LogInformation($"consumer received {content}");
    //    }
    //    private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
    //    private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
    //    private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
    //    private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
    //    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }

    //    public override void Dispose()
    //    {
    //        _channel.Close();
    //        _connection.Close();
    //        base.Dispose();
    //    }
    //    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    //    {

    //        //var candles = await _candleRepository.GetAllCandles();
    //        //foreach (var candle in candles)
    //        //{
    //        //    candle.AveragePrice = (candle.Open + candle.Close + candle.High + candle.Low) / 4;
    //        //    await _candleRepository.UpdateCandle(candle);
    //        //}


    //        //while (!stoppingToken.IsCancellationRequested)
    //        //{
    //        //    if (_logger.IsEnabled(LogLevel.Information))
    //        //    {
    //        //        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
    //        //    }
    //        //    await Task.Delay(1000, stoppingToken);
    //        //}

    //        stoppingToken.ThrowIfCancellationRequested();

    //        var consumer = new EventingBasicConsumer(_channel);
    //        consumer.Received += (ch, ea) =>
    //        {
    //            // received message
    //            var body = ea.Body.ToArray();
    //            var message = Encoding.UTF8.GetString(body);
    //            var candle = JsonSerializer.Deserialize<Candle>(message);

    //            candle.Average = (candle.Open + candle.Close + candle.High + candle.Low) / 4;
    //            _context.Candles.Add(candle);
    //            await _context.SaveChangesAsync();
    //            // handle the received message
    //            HandleMessage(body);
    //            _channel.BasicAck(ea.DeliveryTag, false);
    //        };

    //        consumer.Shutdown += OnConsumerShutdown;
    //        consumer.Registered += OnConsumerRegistered;
    //        consumer.Unregistered += OnConsumerUnregistered;
    //        consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

    //        _channel.BasicConsume("demo.queue.log", false, consumer);
    //        //return Task.CompletedTask;


    //        ConnectionFactory connectionFactory = new ConnectionFactory
    //        {
    //            HostName = "localhost",
    //            UserName = "guest",
    //            Password = "guest",
    //        };
    //        var connection = connectionFactory.CreateConnection();
    //        var channel = connection.CreateModel();

    //        // accept only one unack-ed message at a time
    //        // uint prefetchSize, ushort prefetchCount, bool global
    //        channel.BasicQos(0, 1, false);
    //        EventingBasicConsumer messageReceiver = new EventingBasicConsumer(channel);
    //        channel.BasicConsume("candles", false, messageReceiver){

    //        }


    //        //_ = _channel.BasicConsume(queue: "candles", autoAck: true, consumer: new EventingBasicConsumer(_channel)
    //        //{

    //        //    // Model = async (model, ea) =>
    //        //    //{
    //        //    //    var body = ea.Body.ToArray();
    //        //    //    var message = Encoding.UTF8.GetString(body);
    //        //    //    var candle = JsonSerializer.Deserialize<Candle>(message);

    //        //    //    // Calculate average
    //        //    //    candle.Average = (candle.Open + candle.Close + candle.High + candle.Low) / 4;
    //        //    //    _context.Candles.Add(candle);
    //        //    //    await _context.SaveChangesAsync();
    //        //    //}
    //        //});

    //        while (!stoppingToken.IsCancellationRequested)
    //        {
    //            await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
    //        }
    //    }
    //}


    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
