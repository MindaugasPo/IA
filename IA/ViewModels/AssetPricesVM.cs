using System.Collections.Generic;
using Types.DTO;

namespace IA.ViewModels
{
    public class AssetPricesVM
    {
        public AssetDto Asset { get; set; }
        public List<AssetPriceDto> AssetPrices { get; set; }
    }
}
