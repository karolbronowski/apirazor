using FinalLabProject.Domain.Events.Listener;
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
            "Listener created: ListenerId={ListenerId}, Name={Name}, UserName={UserName}",
            notification.Entity.Id,
            notification.Entity.Name,
            notification.Entity.UserName
        );
        return Task.CompletedTask;
    }
}