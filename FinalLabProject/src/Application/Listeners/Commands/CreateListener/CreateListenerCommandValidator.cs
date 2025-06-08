using FluentValidation;

namespace FinalLabProject.Application.Listeners.Commands.CreateListener;

public class CreateListenerCommandValidator : AbstractValidator<CreateListenerCommand>
{
    public CreateListenerCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(v => v.Username)
            .NotEmpty()
            .WithMessage("Username is required.");

        RuleFor(v => v.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress();

        RuleFor(v => v.Password)
            .NotEmpty()
            .WithMessage("Password is required. Because when there is no user account, a new one will be created.")
            .MinimumLength(4)
            .WithMessage("Password must be at least 4 characters long.")
            .When(v => string.IsNullOrEmpty(v.UserId));

        RuleFor(v => v.UserId)
            .Null()
            .WithMessage("UserId is not allowed when Password is provided.")
            .When(v => !string.IsNullOrEmpty(v.Password));
    }
}