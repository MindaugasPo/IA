using System;
using System.Linq;
using FluentValidation;

namespace ValidationService
{

    public interface IAValidator
    {
        bool IsValid();
        string Errors();
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

        public string Errors()
        {
            var errors = Validate(_objectToValidate).Errors.Select(x => x.ErrorMessage);
            return String.Join(" ", errors);
        }
    }
}
