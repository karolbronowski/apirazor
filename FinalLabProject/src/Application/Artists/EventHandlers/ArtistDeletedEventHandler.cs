using FinalLabProject.Domain.Events.Artist;
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
            "Artist deleted: ArtistId={ArtistId}, Name={Name}, UserName={UserName}",
            notification.Entity.Id,
            notification.Entity.Name,
            notification.Entity.UserName
        );
        return Task.CompletedTask;
    }
}