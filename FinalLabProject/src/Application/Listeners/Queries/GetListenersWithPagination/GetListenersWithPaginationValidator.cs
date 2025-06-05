using FluentValidation;

namespace FinalLabProject.Application.Listeners.Queries.GetListenersWithPagination;

public class GetListenersWithPaginationQueryValidator : AbstractValidator<GetListenersWithPaginationQuery>
{
    public GetListenersWithPaginationQueryValidator()
    {
        RuleFor(v => v.PageNumber)
            .GreaterThan(0)
            .WithMessage("PageNumber must be greater than 0.");

        RuleFor(v => v.PageSize)
            .InclusiveBetween(1, 100)
            .WithMessage("PageSize must be between 1 and 100.");
    }
}