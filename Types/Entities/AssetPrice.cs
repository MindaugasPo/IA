using System;

namespace Types.Entities
{
    public class AssetPrice : EntityBase
    {
        public DateTime Date { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal ClosePrice { get; set; }

        public Guid AssetId { get; set; }
        public Asset Asset { get; set; }
    }
}
