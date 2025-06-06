using FluentValidation;

namespace FinalLabProject.Application.Songs.Commands.PlaySong;

public class PlaySongCommandValidator : AbstractValidator<PlaySongCommand>
{
    public PlaySongCommandValidator()
    {
        RuleFor(v => v.SongId)
            .GreaterThan(0);
    }
}