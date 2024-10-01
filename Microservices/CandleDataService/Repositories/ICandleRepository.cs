using CandleDataService.Models;

namespace CandleDataService.Repositories
{
    public interface ICandleRepository
    {
        Task<IEnumerable<Candle>> GetAllCandles();
        Task<Candle?> GetCandleById(int id);
        Task<Candle> CreateCandle(Candle candle);
        Task DeleteCandle(int id);
        Task UpdateCandle(Candle candle);
    }
}