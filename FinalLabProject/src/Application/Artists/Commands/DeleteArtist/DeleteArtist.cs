using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Domain.Entities;
using FinalLabProject.Domain.Events.Artist;
using MediatR;

namespace FinalLabProject.Application.Artists.Commands.DeleteArtist;

public record DeleteArtistCommand(int Id) : IRequest;

public class DeleteArtistCommandHandler : IRequestHandler<DeleteArtistCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteArtistCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteArtistCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Artists.FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Artists.Remove(entity);

        entity.AddDomainEvent(new ArtistDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}