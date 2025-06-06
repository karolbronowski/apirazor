using FluentValidation;

namespace FinalLabProject.Application.Listeners.Commands.UpdateListener;

public class UpdateListenerCommandValidator : AbstractValidator<UpdateListenerCommand>
{
    public UpdateListenerCommandValidator()
    {
        RuleFor(v => v.Id)
            .GreaterThan(0)
            .WithMessage("Listener Id must be greater than 0.");

        RuleFor(v => v.Name)
            .MaximumLength(100)
            .When(v => v.Name is not null);
    }
}