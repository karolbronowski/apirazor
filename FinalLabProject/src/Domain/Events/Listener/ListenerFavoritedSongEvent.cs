using MediatR;

namespace FinalLabProject.Domain.Events.Listener;

public class ListenerFavoritedSongEvent : BaseEvent
{
    public int ListenerId { get; }
    public int SongId { get; }

    public ListenerFavoritedSongEvent(int listenerId, int songId)
    {
        ListenerId = listenerId;
        SongId = songId;
    }
}