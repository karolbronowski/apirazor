using FinalLabProject.Domain.Events.ListenerEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinalLabProject.Application.Listeners.EventHandlers;

public class ListenerDeletedEventHandler : INotificationHandler<ListenerDeletedEvent>
{
    private readonly ILogger<ListenerDeletedEventHandler> _logger;

    public ListenerDeletedEventHandler(ILogger<ListenerDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ListenerDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Listener deleted: ListenerId={ListenerId}, Name={Name}, Username={Username}",
            notification.Entity.Id,
            notification.Entity.Name,
            notification.Entity.Username
        );
        return Task.CompletedTask;
    }
}