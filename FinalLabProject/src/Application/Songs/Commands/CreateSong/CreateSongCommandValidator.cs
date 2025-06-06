using FluentValidation;

namespace FinalLabProject.Application.Songs.Commands.CreateSong;

public class CreateSongCommandValidator : AbstractValidator<CreateSongCommand>
{
    public CreateSongCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.ArtistId)
            .GreaterThan(0);
    }
}