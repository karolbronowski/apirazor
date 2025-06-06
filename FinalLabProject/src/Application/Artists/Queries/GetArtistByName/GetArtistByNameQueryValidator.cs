using FluentValidation;

namespace FinalLabProject.Application.Artists.Queries.GetArtistByName;

public class GetArtistByNameQueryValidator : AbstractValidator<GetArtistByNameQuery>
{
    public GetArtistByNameQueryValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .WithMessage("Name must not be empty.");
    }
}