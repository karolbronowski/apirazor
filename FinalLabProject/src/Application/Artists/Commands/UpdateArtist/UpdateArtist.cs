using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Domain.Entities;
using FinalLabProject.Domain.Events.Artist;
using MediatR;

namespace FinalLabProject.Application.Artists.Commands.UpdateArtist;

public record UpdateArtistCommand : IRequest
{
  public int Id { get; init; }
  public string? Bio { get; init; }
  public string? Name { get; init; }
}

public class UpdateArtistCommandHandler : IRequestHandler<UpdateArtistCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateArtistCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateArtistCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Artists.FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        if (request.Bio is not null)
            entity.Bio = request.Bio;
        if (request.Name is not null)
            entity.Name = request.Name;

        entity.AddDomainEvent(new ArtistUpdatedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}