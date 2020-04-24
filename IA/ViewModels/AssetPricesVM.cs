using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Types.DTO;

namespace IA.ViewModels
{
    public class AssetPricesVM
    {
        public AssetDto Asset { get; set; }
        public List<AssetPriceDto> AssetPrices { get; set; }
    }
}
