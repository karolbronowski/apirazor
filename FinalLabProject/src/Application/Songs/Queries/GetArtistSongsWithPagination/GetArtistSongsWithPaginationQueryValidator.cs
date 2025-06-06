using FluentValidation;

namespace FinalLabProject.Application.Songs.Queries.GetArtistSongsWithPagination;

public class GetArtistSongsWithPaginationQueryValidator : AbstractValidator<GetArtistSongsWithPaginationQuery>
{
    public GetArtistSongsWithPaginationQueryValidator()
    {
        RuleFor(v => v.ArtistId)
            .GreaterThan(0)
            .WithMessage("ArtistId must be greater than 0.");

        RuleFor(v => v.PageNumber)
            .GreaterThan(0)
            .WithMessage("PageNumber must be greater than 0.");

        RuleFor(v => v.PageSize)
            .InclusiveBetween(1, 100)
            .WithMessage("PageSize must be between 1 and 100.");
    }
}