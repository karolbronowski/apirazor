using FluentValidation;

namespace FinalLabProject.Application.Artists.Queries.GetArtistsWithPagination;

public class GetArtistsWithPaginationQueryValidator : AbstractValidator<GetArtistsWithPaginationQuery>
{
    public GetArtistsWithPaginationQueryValidator()
    {
        RuleFor(v => v.PageNumber)
            .GreaterThan(0)
            .WithMessage("PageNumber must be greater than 0.");

        RuleFor(v => v.PageSize)
            .InclusiveBetween(1, 100)
            .WithMessage("PageSize must be between 1 and 100.");
    }
}