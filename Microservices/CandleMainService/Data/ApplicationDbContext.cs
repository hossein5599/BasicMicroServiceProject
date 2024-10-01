using CandleMainService.Models;
using Microsoft.EntityFrameworkCore;

namespace CandleMainService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Candle> Candles { get; set; }
    }
}
