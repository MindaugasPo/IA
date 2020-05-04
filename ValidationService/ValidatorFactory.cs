using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Types.DTO;

namespace ValidationService
{
    public interface IAValidatorFactory
    {
        IAValidator GetValidator(object o);
    }

    public class ValidatorFactory : IAValidatorFactory
    {
        public IAValidator GetValidator(object o)
        {
            return o is TransactionDto dto ? new TransactionDtoValidator(dto) : null;
        }
    }
}
