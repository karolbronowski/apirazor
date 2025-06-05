using FluentValidation;

namespace FinalLabProject.Application.Artists.Queries.GetArtistById;

public class GetArtistByIdQueryValidator : AbstractValidator<GetArtistByIdQuery>
{
    public GetArtistByIdQueryValidator()
    {
        RuleFor(v => v.Id)
            .GreaterThan(0)
            .WithMessage("Artist Id must be greater than 0.");
    }
}