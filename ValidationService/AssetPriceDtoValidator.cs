using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Types.DTO;

namespace ValidationService
{
    public class AssetPriceDtoValidator : BaseIAValidator<AssetPriceDto>
    {
        public AssetPriceDtoValidator(AssetPriceDto assetPriceDto)
            : base(assetPriceDto)
        {
            RuleFor(x => x.Date).NotEqual(DateTime.MinValue);
        }
    }
}
