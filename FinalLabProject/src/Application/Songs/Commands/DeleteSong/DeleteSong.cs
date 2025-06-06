using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Domain.Events.SongEvents;
using FinalLabProject.Domain.Exceptions.SongExceptions;

namespace FinalLabProject.Application.Songs.Commands.DeleteSong;

public record DeleteSongCommand(int Id) : IRequest;

public class DeleteSongCommandHandler : IRequestHandler<DeleteSongCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteSongCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteSongCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Songs
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if(entity == null)
            throw new SongNotFoundException(request.Id);

        _context.Songs.Remove(entity);

        entity.AddDomainEvent(new SongDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }

}
