using MediatR;

namespace FinalLabProject.Domain.Events.Song;

public class SongUpdatedEvent : BaseEvent
{
    public int SongId { get; }

    public SongUpdatedEvent(int songId)
    {
        SongId = songId;
    }
}