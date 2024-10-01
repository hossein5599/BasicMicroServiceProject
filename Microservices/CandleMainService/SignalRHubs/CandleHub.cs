using CandleMainService.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.SignalR;

namespace CandleMainService.SignalRHubs
{
    public class CandleHub : Hub 
    {
        //private readonly IHubContext<CandleHub> _hubContext;

        //public CandleHub(IHubContext<CandleHub> hubContext)
        //{
        //    _hubContext = hubContext;
        //    StartListening();
        //}

        //private void StartListening()
        //{
        //    var factory = new ConnectionFactory() { HostName = "localhost" };
        //    using var connection = factory.CreateConnection();
        //    using var channel = connection.CreateModel();
        //    channel.QueueDeclare(queue: "candle_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
        //    var consumer = new EventingBasicConsumer(channel);
        //    consumer.Received += async (model, ea) =>
        //    {
        //        var json = Encoding.UTF8.GetString(ea.Body.ToArray());
        //        var candle = JsonSerializer.Deserialize<Candle>(json);
        //        await _hubContext.Clients.All.SendAsync("ReceiveCandle", candle);
        //    };
        //    channel.BasicConsume(queue: "candle_queue", autoAck: true, consumer: consumer);
        //}


        //=======================================================================
        //private readonly IHubContext<CandleHub> _hubContext;
        //private Timer _candleGenerationTimer;
        //private Random _random;

        //public CandleHub(IHubContext<CandleHub> hubContext)
        //{
        //    _hubContext = hubContext;
        //    _random = new Random();
        //    StartGeneratingCandleData();
        //}

        //private void StartGeneratingCandleData()
        //{
        //    _candleGenerationTimer = new Timer(2000); // 2 seconds interval
        //    _candleGenerationTimer.Elapsed += async (sender, e) => await SendCandleData();
        //    _candleGenerationTimer.AutoReset = true;
        //    _candleGenerationTimer.Enabled = true;
        //}

        //private async Task SendCandleData()
        //{
        //    var candle = GenerateRandomCandle();
        //    await _hubContext.Clients.All.SendAsync("ReceiveCandle", candle);
        //}

        //private Candle GenerateRandomCandle()
        //{
        //    return new Candle
        //    {
        //        Id = Guid.NewGuid().GetHashCode(), // Unique id
        //        Open = GetRandomDecimal(100, 200),
        //        Close = GetRandomDecimal(100, 200),
        //        High = GetRandomDecimal(200, 300),
        //        Low = GetRandomDecimal(50, 100),
        //        Average = GetRandomDecimal(100, 200),
        //        CreatedAt = DateTime.UtcNow
        //    };
        //}

        //private decimal GetRandomDecimal(double minValue, double maxValue)
        //{
        //    return (decimal)(_random.NextDouble() * (maxValue - minValue) + minValue);
        //}

        //public override Task OnDisconnectedAsync(System.Net.WebSockets.WebSocketCloseStatus closeStatus)
        //{
        //    // Clean up resources if needed
        //    _candleGenerationTimer?.Stop();
        //    _candleGenerationTimer?.Dispose();
        //    return base.OnDisconnectedAsync(closeStatus);
        //}

    }
}
