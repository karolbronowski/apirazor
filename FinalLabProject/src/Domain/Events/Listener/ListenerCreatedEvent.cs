﻿using MediatR;

namespace FinalLabProject.Domain.Events;

public class ListenerCreatedEvent : BaseEvent
{
    public int ListenerId { get; }

    public ListenerCreatedEvent(int listenerId)
    {
        ListenerId = listenerId;
    }
}