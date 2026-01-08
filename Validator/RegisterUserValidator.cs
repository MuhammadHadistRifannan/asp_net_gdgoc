using FluentValidation;

namespace gdgoc_aspnet;

public sealed class RegisterUserValidator : AbstractValidator<User>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.password)
        .NotEmpty()
        .Matches(@"[A-Z]+").WithMessage("Password Must contain single upper case character")
        .Matches(@"[a-z]+").WithMessage("Password Must contain single lower case character")
        .Matches(@"[0-9]+").WithMessage("Password Must contain number")
        .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|\'~\\-]").WithMessage("Password Must contain special character");

        RuleFor(x => x.email).NotEmpty().WithMessage("Email should not empty");
        RuleFor(x => x.first_name).NotEmpty(); 
        RuleFor(x => x.last_name).NotEmpty();
    }
}
