using MediatR;

namespace FinalLabProject.Domain.Events.Listener;

public class ListenerUpdatedEvent : BaseEvent
{
    public int ListenerId { get; }

    public ListenerUpdatedEvent(int listenerId)
    {
        ListenerId = listenerId;
    }
}