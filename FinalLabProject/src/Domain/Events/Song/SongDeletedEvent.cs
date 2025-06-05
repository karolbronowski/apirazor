using FinalLabProject.Domain.Common;
using FinalLabProject.Domain.Entities;

namespace FinalLabProject.Domain.Events.Song;

public class SongDeletedEvent : EntityEvent<Song>
{
    public SongDeletedEvent(Song song)
        : base(song, EntityEventType.Deleted)
    {
    }
}