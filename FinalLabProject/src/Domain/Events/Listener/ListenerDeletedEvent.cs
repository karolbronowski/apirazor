using MediatR;

namespace FinalLabProject.Domain.Events.Listener;

public class ListenerDeletedEvent : BaseEvent
{
    public int ListenerId { get; }

    public ListenerDeletedEvent(int listenerId)
    {
        ListenerId = listenerId;
    }
}