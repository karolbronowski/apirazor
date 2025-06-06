using FinalLabProject.Domain.Events.ArtistEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinalLabProject.Application.Artists.EventHandlers;

public class ArtistCreatedEventHandler : INotificationHandler<ArtistCreatedEvent>
{
    private readonly ILogger<ArtistCreatedEventHandler> _logger;

    public ArtistCreatedEventHandler(ILogger<ArtistCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ArtistCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Artist created: ArtistId={ArtistId}, Name={Name}, Username={Username}",
            notification.Entity.Id,
            notification.Entity.Name,
            notification.Entity.Username
        );
        return Task.CompletedTask;
    }
}