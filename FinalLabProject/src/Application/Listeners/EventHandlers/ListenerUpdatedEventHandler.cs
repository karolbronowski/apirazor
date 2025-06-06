using FinalLabProject.Domain.Events.Listener;
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
            "Listener updated: ListenerId={ListenerId}, Name={Name}, UserName={UserName}",
            notification.Entity.Id,
            notification.Entity.Name,
            notification.Entity.UserName
        );
        return Task.CompletedTask;
    }
}