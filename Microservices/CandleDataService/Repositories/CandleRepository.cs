using CandleDataService.Data;
using CandleDataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandleDataService.Repositories
{
    public class CandleRepository : ICandleRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CandleRepository(ApplicationDbContext dbContext) => this.dbContext = dbContext;

        public async Task<Candle> CreateCandle(Candle candle)
        {
            try
            {
            await this.dbContext.candles.AddAsync(candle);
            await this.dbContext.SaveChangesAsync();
            return candle;
            }
            catch (Exception)
            {
                
                return null;
            }

        }
 
        public Task DeleteCandle(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Candle>> GetAllCandles()
        {
            throw new NotImplementedException();
        }

        public Task<Candle?> GetCandleById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCandle(Candle candle)
        {
            throw new NotImplementedException();
        }
    }
}
