using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Types.DTO;

namespace ValidationService
{
    public class PortfolioDtoValidator : BaseIAValidator<PortfolioDto>
    {
        public PortfolioDtoValidator(PortfolioDto portfolio)
        :base(portfolio)
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(50);
            RuleFor(x => x.UserId).NotEmpty().MaximumLength(450);
        }
    }
}
