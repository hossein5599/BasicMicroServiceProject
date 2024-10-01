using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandleDataService.Models.Dtos;
public class CandleDto
{
    public int Open { get; set; }
    public int Close { get; set; }
    public int High { get; set; }
    public int Low { get; set; }
}

