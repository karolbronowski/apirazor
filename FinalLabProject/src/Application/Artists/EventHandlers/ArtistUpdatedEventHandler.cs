using FinalLabProject.Domain.Events.ArtistEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinalLabProject.Application.Artists.EventHandlers;

public class ArtistUpdatedEventHandler : INotificationHandler<ArtistUpdatedEvent>
{
    private readonly ILogger<ArtistUpdatedEventHandler> _logger;

    public ArtistUpdatedEventHandler(ILogger<ArtistUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ArtistUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Artist updated: ArtistId={ArtistId}, Name={Name}, Username={Username}",
            notification.Entity.Id,
            notification.Entity.Name,
            notification.Entity.Username
        );
        return Task.CompletedTask;
    }
}