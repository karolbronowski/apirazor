using MediatR;

namespace FinalLabProject.Domain.Events.Artist;

public class ArtistUpdatedEvent : BaseEvent
{
    public int ArtistId { get; }

    public ArtistUpdatedEvent(int artistId)
    {
        ArtistId = artistId;
    }
}