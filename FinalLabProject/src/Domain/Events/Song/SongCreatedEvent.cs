using FinalLabProject.Domain.Common;
using FinalLabProject.Domain.Entities;

namespace FinalLabProject.Domain.Events.Song;

public class SongCreatedEvent : EntityEvent<Song>
{
    public SongCreatedEvent(Song song)
        : base(song, EntityEventType.Created)
    {
    }
}