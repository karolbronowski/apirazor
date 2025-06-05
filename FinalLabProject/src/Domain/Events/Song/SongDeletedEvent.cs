using MediatR;

namespace FinalLabProject.Domain.Events.Song;

public class SongDeletedEvent : BaseEvent
{
    public int SongId { get; }

    public SongDeletedEvent(int songId)
    {
        SongId = songId;
    }
}