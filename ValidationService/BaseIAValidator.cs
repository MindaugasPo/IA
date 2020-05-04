using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace ValidationService
{

    public interface IAValidator
    {
        bool IsValid();
    }
    public class BaseIAValidator<T> : AbstractValidator<T>, IAValidator where T : class
    {
        private readonly T _objectToValidate;

        public BaseIAValidator(T objectToValidate)
        {
            _objectToValidate = objectToValidate;
        }
        public bool IsValid()
        {
            return Validate(_objectToValidate).IsValid;
        }
    }
}
