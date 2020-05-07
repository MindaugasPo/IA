using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Types.DTO;

namespace ValidationService
{
    public class AssetDtoValidator : BaseIAValidator<AssetDto>
    {
        public AssetDtoValidator(AssetDto asset)
        :base(asset)
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Ticker).NotEmpty();
        }
    }
}
