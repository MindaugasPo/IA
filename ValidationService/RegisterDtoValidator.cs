using FluentValidation;
using Types.DTO;

namespace ValidationService
{
    public class RegisterDtoValidator : BaseIAValidator<RegisterDto>
    {
        public RegisterDtoValidator(RegisterDto registerDto)
        : base(registerDto)
        {
            RuleFor(x => x.Username).NotEmpty().EmailAddress().MaximumLength(256);
            RuleFor(x => x.Password).NotEmpty().MaximumLength(100);
        }
    }
}
