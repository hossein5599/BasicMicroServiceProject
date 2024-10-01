using CandleDataService.Models;
using CandleDataService.Models.Dtos;
using CandleDataService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandleDataService.Services
{
    public class CandleService : ICandleService
    {
        private readonly ICandleRepository candleRepository;
        private readonly IConfiguration configuration;

        public CandleService(IConfiguration configuration, ICandleRepository candleRepository)
        {
            this.configuration = configuration;
            this.candleRepository = candleRepository;
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
            var savedCandle = await this.candleRepository.CreateCandle(candle);
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
    }
}
