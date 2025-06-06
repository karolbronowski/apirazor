using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Domain.Entities;
using FinalLabProject.Domain.Events.Song;
using MediatR;

namespace FinalLabProject.Application.Songs.Commands.PlaySong;

public record PlaySongCommand(int SongId) : IRequest;

public class PlaySongCommandHandler : IRequestHandler<PlaySongCommand>
{
    private readonly IApplicationDbContext _context;

    public PlaySongCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(PlaySongCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Songs
            .FindAsync(new object[] { request.SongId }, cancellationToken);

        Guard.Against.NotFound(request.SongId, entity);

        entity.ListenedTimes += 1;
        entity.AddDomainEvent(new SongPlayedEvent(entity.Id));

        await _context.SaveChangesAsync(cancellationToken);
    }
}