using FluentValidation;

namespace FinalLabProject.Application.Listeners.Commands.UpdateFavoritedSongListener;

public class UpdateFavoritedSongListenerCommandValidator : AbstractValidator<UpdateFavoritedSongListenerCommand>
{
    public UpdateFavoritedSongListenerCommandValidator()
    {
        RuleFor(v => v.ListenerId)
            .GreaterThan(0)
            .WithMessage("ListenerId must be greater than 0.");

        RuleFor(v => v.SongId)
            .GreaterThan(0)
            .WithMessage("SongId must be greater than 0.");
    }
}