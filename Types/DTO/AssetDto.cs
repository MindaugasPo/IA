using System;
using System.Collections.Generic;

namespace Types.DTO
{
    public class AssetDto : BaseDto
    {
        public String Title { get; set; }
        public String Ticker { get; set; }
        public IAenums.AssetType AssetType { get; set; }

        public List<TransactionDto> Transactions { get; set; }
        public List<AssetPriceDto> AssetPrices { get; set; }
    }
}
