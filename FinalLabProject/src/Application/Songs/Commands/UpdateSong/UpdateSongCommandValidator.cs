using FluentValidation;

namespace FinalLabProject.Application.Songs.Commands.UpdateSong;

public class UpdateSongCommandValidator : AbstractValidator<UpdateSongCommand>
{
    public UpdateSongCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .When(v => v.Title is not null);

        RuleFor(v => v.ArtistId)
            .GreaterThan(0)
            .When(v => v.ArtistId.HasValue);
    }
}