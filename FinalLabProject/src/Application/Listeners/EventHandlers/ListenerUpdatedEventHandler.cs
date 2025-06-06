using FinalLabProject.Domain.Events.ListenerEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinalLabProject.Application.Listeners.EventHandlers;

public class ListenerUpdatedEventHandler : INotificationHandler<ListenerUpdatedEvent>
{
    private readonly ILogger<ListenerUpdatedEventHandler> _logger;

    public ListenerUpdatedEventHandler(ILogger<ListenerUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ListenerUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Listener updated: ListenerId={ListenerId}, Name={Name}, Username={Username}",
            notification.Entity.Id,
            notification.Entity.Name,
            notification.Entity.Username
        );
        return Task.CompletedTask;
    }
}