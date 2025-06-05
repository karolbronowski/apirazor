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
    }
}