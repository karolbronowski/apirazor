using FinalLabProject.Domain.Events.ArtistEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinalLabProject.Application.Artists.EventHandlers;

public class ArtistDeletedEventHandler : INotificationHandler<ArtistDeletedEvent>
{
    private readonly ILogger<ArtistDeletedEventHandler> _logger;

    public ArtistDeletedEventHandler(ILogger<ArtistDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ArtistDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Artist deleted: ArtistId={ArtistId}, Name={Name}, Username={Username}",
            notification.Entity.Id,
            notification.Entity.Name,
            notification.Entity.Username
        );
        return Task.CompletedTask;
    }
}