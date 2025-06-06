using FinalLabProject.Domain.Events.Song;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinalLabProject.Application.Songs.EventHandlers;

public class SongUpdatedEventHandler : INotificationHandler<SongUpdatedEvent>
{
    private readonly ILogger<SongUpdatedEventHandler> _logger;

    public SongUpdatedEventHandler(ILogger<SongUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(SongUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Song updated: {SongId}, Title: {Title}", notification.Entity.Id, notification.Entity.Title);
        return Task.CompletedTask;
    }
}