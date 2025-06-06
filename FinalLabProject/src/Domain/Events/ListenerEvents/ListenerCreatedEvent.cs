﻿using FinalLabProject.Domain.Common;
using FinalLabProject.Domain.Entities;

namespace FinalLabProject.Domain.Events.ListenerEvents;

public class ListenerCreatedEvent : EntityEvent<Listener>
{
    public ListenerCreatedEvent(Listener listener)
        : base(listener, EntityEventType.Created)
    {
    }
}