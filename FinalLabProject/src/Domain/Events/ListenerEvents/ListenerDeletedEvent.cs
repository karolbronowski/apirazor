using FinalLabProject.Domain.Common;
using FinalLabProject.Domain.Entities;

namespace FinalLabProject.Domain.Events.ListenerEvents;

public class ListenerDeletedEvent : EntityEvent<Listener>
{
    public ListenerDeletedEvent(Listener listener)
        : base(listener, EntityEventType.Deleted)
    {
    }
}