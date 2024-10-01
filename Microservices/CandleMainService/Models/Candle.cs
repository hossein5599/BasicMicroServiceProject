namespace CandleMainService.Models;
public class Candle
{
    public int Id { get; set; }
    public decimal Open { get; set; }
    public decimal Close { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal? Average { get; set; }
    public DateTime CreatedAt { get; internal set; }
}