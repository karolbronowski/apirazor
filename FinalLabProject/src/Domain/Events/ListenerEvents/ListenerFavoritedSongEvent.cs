using MediatR;

namespace FinalLabProject.Domain.Events.ListenerEvents;

public class ListenerFavoritedSongEvent : BaseEvent
{
    public int ListenerId { get; }
    public int SongId { get; }

    public bool IsAdded { get; }

    public ListenerFavoritedSongEvent(int listenerId, int songId, bool isAdded)
    {
        ListenerId = listenerId;
        SongId = songId;
        IsAdded = isAdded;
    }
}