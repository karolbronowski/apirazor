using MediatR;

namespace FinalLabProject.Domain.Events.Song;

public class SongCreatedEvent : BaseeEvent
{
    public int SongId { get; }

    public SongCreatedEvent(int songId)
    {
        SongId = songId;
    }
}