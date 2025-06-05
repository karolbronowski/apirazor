using MediatR;

namespace FinalLabProject.Domain.Events.Artist;

public class ArtistDeletedEvent : BaseEvent
{
    public int ArtistId { get; }

    public ArtistDeletedEvent(int artistId)
    {
        ArtistId = artistId;
    }
}