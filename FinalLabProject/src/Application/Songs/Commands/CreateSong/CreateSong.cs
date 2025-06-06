using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Domain.Entities;
using FinalLabProject.Domain.Events.SongEvents;
using FinalLabProject.Domain.Exceptions.SongExceptions;
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
        // Check if a song with the same title already exists for this artist
        if (_context.Songs.Any(s => s.Title == request.Title && s.ArtistId == request.ArtistId))
            throw new SongAlreadyExistsException(request.Title, request.ArtistId);
    
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