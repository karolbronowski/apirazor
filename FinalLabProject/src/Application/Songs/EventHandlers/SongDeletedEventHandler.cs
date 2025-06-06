using FinalLabProject.Domain.Events.SongEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinalLabProject.Application.Songs.EventHandlers;

public class SongDeletedEventHandler : INotificationHandler<SongDeletedEvent>
{
    private readonly ILogger<SongDeletedEventHandler> _logger;

    public SongDeletedEventHandler(ILogger<SongDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(SongDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Song deleted: {SongId}, Title: {Title}", notification.Entity.Id, notification.Entity.Title);
        return Task.CompletedTask;
    }
}