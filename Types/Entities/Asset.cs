using System;
using System.Collections.Generic;

namespace Types.Entities
{
    public class Asset : EntityBase
    {
        public String Title { get; set; }
        public String Ticker { get; set; }
        public IAenums.AssetType AssetType { get; set; }

        public List<Transaction> Transactions { get; set; }
        public List<AssetPrice> AssetPrices { get; set; }
    }
}
