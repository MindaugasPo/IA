using System;
using System.Collections.Generic;
using System.Text;

namespace Types.DTO
{
    public class AssetPriceDto
    {
        public DateTime Date { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public AssetDto Asset { get; set; }
    }
}
