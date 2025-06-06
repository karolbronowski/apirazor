using FluentValidation;

namespace FinalLabProject.Application.Listeners.Commands.CreateListener;

public class CreateListenerCommandValidator : AbstractValidator<CreateListenerCommand>
{
    public CreateListenerCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(v => v.UserName)
            .NotEmpty()
            .WithMessage("Username is required.");
        RuleFor(v => v.Email)
            .NotEmpty()
            .WithMessage("Email is required.");
        RuleFor(v => v.PasswordHash)
            .NotEmpty()
            .WithMessage("Password hash is required.");
    }
}