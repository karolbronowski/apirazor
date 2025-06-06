using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Domain.Entities;
using FinalLabProject.Domain.Events.SongEvents;
using MediatR;

namespace FinalLabProject.Application.Songs.Commands.UpdateSong;

public record UpdateSongCommand : IRequest
{
    public int Id { get; init; }
    public string? Title { get; init; }
    public int? ArtistId { get; init; }
}

public class UpdateSongCommandHandler : IRequestHandler<UpdateSongCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateSongCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateSongCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Songs
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        if (request.Title is not null)
            entity.Title = request.Title;

        if (request.ArtistId.HasValue)
            entity.ArtistId = request.ArtistId.Value;

        entity.AddDomainEvent(new SongUpdatedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}