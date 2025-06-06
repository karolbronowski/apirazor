using FinalLabProject.Domain.Common;
using FinalLabProject.Domain.Entities;

namespace FinalLabProject.Domain.Events.SongEvents;

public class SongUpdatedEvent : EntityEvent<Song>
{
    public SongUpdatedEvent(Song song)
        : base(song, EntityEventType.Updated)
    {
    }
}