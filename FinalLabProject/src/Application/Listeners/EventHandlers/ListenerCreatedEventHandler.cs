using FinalLabProject.Domain.Events.ListenerEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinalLabProject.Application.Listeners.EventHandlers;

public class ListenerCreatedEventHandler : INotificationHandler<ListenerCreatedEvent>
{
    private readonly ILogger<ListenerCreatedEventHandler> _logger;

    public ListenerCreatedEventHandler(ILogger<ListenerCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ListenerCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Listener created: ListenerId={ListenerId}, Name={Name}, Username={Username}",
            notification.Entity.Id,
            notification.Entity.Name,
            notification.Entity.Username
        );
        return Task.CompletedTask;
    }
}