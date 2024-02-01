using FluentValidation;
using SoclukProject.Common.Models.RequestModels;

namespace SoclukProject.Api.Application.Features.Commands.User;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(c => c.EmailAddress)
            .NotNull()
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("{PropertyName} a not valid email address");

        RuleFor(c => c.Password)
            .NotNull()
            .MinimumLength(6)
            .WithMessage("{PropertyName} should at least be {MinLenght} characters");
    }
}

