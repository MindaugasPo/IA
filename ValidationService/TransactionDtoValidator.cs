using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Types.DTO;

namespace ValidationService
{
    public class TransactionDtoValidator : BaseIAValidator<TransactionDto>
    {
        public TransactionDtoValidator(TransactionDto transaction)
        : base(transaction)
        {
            RuleFor(x => x.OpenDateUtc).NotEqual(DateTime.MinValue);
            RuleFor(x => x.Amount).GreaterThan(0);
        }
    }
}
