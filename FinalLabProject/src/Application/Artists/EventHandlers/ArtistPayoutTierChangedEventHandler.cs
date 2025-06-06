using FinalLabProject.Domain.Events.Artist;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinalLabProject.Application.Artists.EventHandlers;

public class ArtistPayoutTierChangedEventHandler : INotificationHandler<ArtistPayoutTierChangedEvent>
{
    private readonly ILogger<ArtistPayoutTierChangedEventHandler> _logger;

    public ArtistPayoutTierChangedEventHandler(ILogger<ArtistPayoutTierChangedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ArtistPayoutTierChangedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Artist payout tier changed: ArtistId={ArtistId}, OldTier={OldTier}, NewTier={NewTier}",
            notification.Artist.Id,
            notification.OldPayoutTier,
            notification.NewPayoutTier
        );
        return Task.CompletedTask;
    }
}