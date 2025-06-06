using FinalLabProject.Domain.Events.Artist;
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
            "Artist created: ArtistId={ArtistId}, Name={Name}, UserName={UserName}",
            notification.Entity.Id,
            notification.Entity.Name,
            notification.Entity.UserName
        );
        return Task.CompletedTask;
    }
}