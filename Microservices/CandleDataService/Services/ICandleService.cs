using CandleDataService.Models;
using CandleDataService.Models.Dtos;

namespace CandleDataService.Services
{
    public interface ICandleService
    {
        Task<Candle> RegisterCandle(CandleDto candleDto);
    }
}