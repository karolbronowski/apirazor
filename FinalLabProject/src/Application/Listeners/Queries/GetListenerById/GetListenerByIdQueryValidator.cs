using FluentValidation;

namespace FinalLabProject.Application.Listeners.Queries.GetListenerById;

public class GetListenerByIdQueryValidator : AbstractValidator<GetListenerByIdQuery>
{
    public GetListenerByIdQueryValidator()
    {
        RuleFor(v => v.Id)
            .GreaterThan(0)
            .WithMessage("Listener Id must be greater than 0.");
    }
}