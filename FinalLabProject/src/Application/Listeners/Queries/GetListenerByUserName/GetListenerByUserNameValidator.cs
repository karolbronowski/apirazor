using FluentValidation;

namespace FinalLabProject.Application.Listeners.Queries.GetListenerByUserName;

public class GetListenerByUserNameQueryValidator : AbstractValidator<GetListenerByUserNameQuery>
{
    public GetListenerByUserNameQueryValidator()
    {
        RuleFor(v => v.UserName)
            .NotEmpty()
            .WithMessage("UserName must not be empty.");
    }
}