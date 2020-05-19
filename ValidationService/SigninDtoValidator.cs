
using FluentValidation;
using Types.DTO;

namespace ValidationService
{
    public class SigninDtoValidator : BaseIAValidator<SigninDto>
    {
        public SigninDtoValidator(SigninDto signinDto)
        : base(signinDto)
        {
            RuleFor(x => x.Username).NotEmpty().EmailAddress().MaximumLength(256);
            RuleFor(x => x.Password).NotEmpty().MaximumLength(100);
        }
    }
}
