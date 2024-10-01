using CandleMainService.Data;
using CandleMainService.Models;
using CandleMainService.SignalRHubs;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace CandleMainService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandlesController : ControllerBase
    {
        private static readonly ConcurrentBag<Candle> Candles = new ConcurrentBag<Candle>();

        [HttpGet]
        public IActionResult GetCandles()
        {
            return Ok(Candles);
        }

        [CapSubscribe("CandleNewAdded")]
        public void Consumer(System.Text.Json.JsonElement candleData)
        {
            var candleJson = candleData.GetRawText();
            var candle = System.Text.Json.JsonSerializer.Deserialize<Candle>(candleJson);

            if (candle != null)
            {
                Candles.Add(candle);
                Console.WriteLine($"Candle received: {candle.Id}");
            }
        }
        //private readonly ApplicationDbContext _context;
        //private readonly IHubContext<CandleHub> _hubContext;

        //public CandlesController(ApplicationDbContext context, IHubContext<CandleHub> hubContext)
        //{
        //    _context = context;
        //    _hubContext = hubContext;
        //}



        //[HttpGet]
        //public async Task<IActionResult> GetCandles()
        //{
        //    var candles = await _context.Candles.ToListAsync();
        //    return Ok(candles);
        //}

        //[HttpPost]
        //public async Task<IActionResult> CreateCandle([FromBody] Candle candle)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Candles.Add(candle);
        //        await _context.SaveChangesAsync();

        //        // Notify clients of the new candle
        //        await _hubContext.Clients.All.SendAsync("ReceiveCandle", candle);
        //        return CreatedAtAction(nameof(GetCandles), new { id = candle.Id }, candle);
        //    }
        //    return BadRequest(ModelState);
        //}
    }
}
