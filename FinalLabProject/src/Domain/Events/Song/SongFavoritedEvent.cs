using MediatR;

namespace FinalLabProject.Domain.Events.Song;

public class SongFavoritedEvent : BaseEvent
{
    public int ListenerId { get; }
    public int SongId { get; }

    public SongFavoritedEvent(int listenerId, int songId)
    {
        ListenerId = listenerId;
        SongId = songId;
    }
}