using MediatR;

namespace FinalLabProject.Domain.Common;

/// <summary>
/// Generic event for signaling changes on entities.
/// </summary>
public class EntityEvent<TEntity> : BaseEvent
{
    public TEntity Entity { get; }
    public EntityEventType EventType { get; }

    public EntityEvent(TEntity entity, EntityEventType eventType)
    {
        Entity = entity;
        EventType = eventType;
    }
}

public enum EntityEventType
{
    Created,
    Updated,
    Deleted
}