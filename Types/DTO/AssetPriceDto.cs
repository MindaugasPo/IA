using System;

namespace Types.DTO
{
    public class AssetPriceDto : BaseDto
    {
        public DateTime Date { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public AssetDto Asset { get; set; }
        public Guid AssetId { get; set; }
    }
}
