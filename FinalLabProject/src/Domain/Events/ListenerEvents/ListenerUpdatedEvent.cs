using FinalLabProject.Domain.Common;
using FinalLabProject.Domain.Entities;

namespace FinalLabProject.Domain.Events.ListenerEvents;

public class ListenerUpdatedEvent : EntityEvent<Listener>
{
    public ListenerUpdatedEvent(Listener listener)
        : base(listener, EntityEventType.Updated)
    {
    }
}