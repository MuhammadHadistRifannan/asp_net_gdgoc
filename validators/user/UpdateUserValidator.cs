using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyApp.Namespace;

namespace gdgoc_aspnet;

public class UpdateUserValidator : AbstractValidator<UserUpdateRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(user => user.first_name)
        .MaximumLength(30);
        
        RuleFor(user => user.last_name)
        .MaximumLength(30);
        
        RuleFor(user => user.password)
        .Matches(@"[A-Z]+").WithMessage("Password Must contain single upper case character")
        .Matches(@"[a-z]+").WithMessage("Password Must contain single lower case character")
        .Matches(@"[0-9]+").WithMessage("Password Must contain number")
        .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|\'~\\-]").WithMessage("Password Must contain special character")
        .Equal(user => user.confirm_password).WithMessage("Password and Confirm password are must equal");
        
    }
}
