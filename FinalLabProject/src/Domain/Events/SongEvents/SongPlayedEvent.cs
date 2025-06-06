using MediatR;

namespace FinalLabProject.Domain.Events.SongEvents;

public class SongPlayedEvent : BaseEvent
{
    public int SongId { get; }

    public SongPlayedEvent(int songId)
    {
        SongId = songId;
    }
}