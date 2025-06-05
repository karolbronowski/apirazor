using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Domain.Entities;
using FinalLabProject.Domain.Events.Song;
using MediatR;

namespace FinalLabProject.Application.Songs.Commands.CreateSong;

public record CreateSongCommand : IRequest<int>
{
    public string Title { get; init; } = default!;
    public int ArtistId { get; init; }
}

public class CreateSongCommandHandler : IRequestHandler<CreateSongCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateSongCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateSongCommand request, CancellationToken cancellationToken)
    {
        var entity = new Song
        {
            Title = request.Title,
            ArtistId = request.ArtistId,
            ListenedTimes = 0
        };

        entity.AddDomainEvent(new SongCreatedEvent(entity));

        _context.Songs.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}