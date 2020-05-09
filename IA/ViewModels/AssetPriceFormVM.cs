using System;
using Types.DTO;

namespace IA.ViewModels
{
    public class AssetPriceFormVM
    {
        public Guid AssetId { get; set; }
        public AssetPriceDto AssetPriceDto { get; set; }
    }
}
