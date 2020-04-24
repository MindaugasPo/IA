using System;
using System.Collections.Generic;
using System.Text;

namespace Types.DTO
{
    public class TransactionDto : BaseDto
    {
        public decimal OpenPrice { get; set; }
        public DateTime OpenDateUtc { get; set; }
        public decimal Amount { get; set; }
        public decimal Commission { get; set; }
        public IAenums.TransactionType TransactionType { get; set; }
        public IAenums.Currency Currency { get; set; }
        public decimal? ClosePrice { get; set; }
        public DateTime? CloseDateUtc { get; set; }
        public AssetDto Asset { get; set; }
    }
}
