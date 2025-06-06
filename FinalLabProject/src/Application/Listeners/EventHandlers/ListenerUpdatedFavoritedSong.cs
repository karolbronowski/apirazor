using FinalLabProject.Domain.Events.Listener;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinalLabProject.Application.Listeners.EventHandlers;

public class ListenerUpdatedFavoritedSongEventHandler : INotificationHandler<ListenerFavoritedSongEvent>
{
    private readonly ILogger<ListenerUpdatedFavoritedSongEventHandler> _logger;

    public ListenerUpdatedFavoritedSongEventHandler(ILogger<ListenerUpdatedFavoritedSongEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ListenerFavoritedSongEvent notification, CancellationToken cancellationToken)
    {
        var action = notification.IsAdded ? "added to" : "removed from";
        _logger.LogInformation(
            "Song {SongId} was {Action} favorites for ListenerId={ListenerId}",
            notification.SongId,
            action,
            notification.ListenerId
        );
        return Task.CompletedTask;
    }
}