using FluentValidation;

namespace gdgoc_aspnet;

public class LoginUserValidator : AbstractValidator<User>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.password)
        .NotEmpty();
        RuleFor(x => x.email).NotEmpty().WithMessage("Email should not empty");
    }
}
