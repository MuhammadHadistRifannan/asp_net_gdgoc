using FluentValidation;
using MyApp.Namespace;

namespace gdgoc_aspnet;

public class LoginUserValidator : AbstractValidator<UserLoginRequest>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.password)
        .NotEmpty();
        RuleFor(x => x.email).NotEmpty().WithMessage("Email should not empty")
        .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
    }
}
