using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Domain.Entities;
using FinalLabProject.Domain.Events.ArtistEvents;
using FinalLabProject.Domain.ValueObjects;
using MediatR;

namespace FinalLabProject.Application.Artists.Commands.UpdateArtistPayoutTier;

public record UpdateArtistPayoutTierCommand : IRequest
{
    public int ArtistId { get; init; }
    public string PayoutTier { get; init; } = default!;
}

public class UpdateArtistPayoutTierCommandHandler : IRequestHandler<UpdateArtistPayoutTierCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateArtistPayoutTierCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateArtistPayoutTierCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Artists.FindAsync(new object[] { request.ArtistId }, cancellationToken);

        Guard.Against.NotFound(request.ArtistId, entity);

        var oldPayoutTier = entity.PayoutTier;
        var newPayoutTier = new PayoutTier(request.PayoutTier);

        if (!oldPayoutTier.Equals(newPayoutTier))
        {
            entity.PayoutTier = newPayoutTier;
            entity.AddDomainEvent(new ArtistPayoutTierChangedEvent(entity, oldPayoutTier, newPayoutTier));
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}