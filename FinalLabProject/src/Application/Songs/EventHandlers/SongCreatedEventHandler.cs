using FinalLabProject.Domain.Events.Song;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinalLabProject.Application.Songs.EventHandlers;

public class SongCreatedEventHandler : INotificationHandler<SongCreatedEvent>
{
    private readonly ILogger<SongCreatedEventHandler> _logger;

    public SongCreatedEventHandler(ILogger<SongCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(SongCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Song created: {SongId}, Title: {Title}", notification.Entity.Id, notification.Entity.Title);
        return Task.CompletedTask;
    }
}