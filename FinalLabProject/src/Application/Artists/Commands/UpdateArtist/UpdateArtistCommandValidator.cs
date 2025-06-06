using FluentValidation;

namespace FinalLabProject.Application.Artists.Commands.UpdateArtist;

public class UpdateArtistCommandValidator : AbstractValidator<UpdateArtistCommand>
{
    public UpdateArtistCommandValidator()
    {
        RuleFor(v => v.Id)
            .GreaterThan(0);

        RuleFor(v => v.Name)
            .MaximumLength(100)
            .When(v => v.Name is not null);

        RuleFor(v => v.Bio)
            .MaximumLength(500)
            .When(v => v.Bio is not null);
    }
}