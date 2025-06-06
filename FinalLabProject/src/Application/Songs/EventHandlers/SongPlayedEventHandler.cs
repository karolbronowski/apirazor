using FinalLabProject.Domain.Events.SongEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinalLabProject.Application.Songs.EventHandlers;

public class SongPlayedEventHandler : INotificationHandler<SongPlayedEvent>
{
    private readonly ILogger<SongPlayedEventHandler> _logger;

    public SongPlayedEventHandler(ILogger<SongPlayedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(SongPlayedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Song played: {SongId}", notification.SongId);
        return Task.CompletedTask;
    }
}