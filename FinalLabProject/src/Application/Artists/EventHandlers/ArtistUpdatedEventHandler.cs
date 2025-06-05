using FinalLabProject.Domain.Events.Artist;
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
            "Artist updated: ArtistId={ArtistId}, Name={Name}, UserName={UserName}",
            notification.Entity.Id,
            notification.Entity.Name,
            notification.Entity.UserName
        );
        return Task.CompletedTask;
    }
}