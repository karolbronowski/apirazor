using FinalLabProject.Domain.Common;
using FinalLabProject.Domain.Entities;

namespace FinalLabProject.Domain.Events.ArtistEvents;

public class ArtistUpdatedEvent : EntityEvent<Artist>
{
    public ArtistUpdatedEvent(Artist artist)
        : base(artist, EntityEventType.Updated)
    {
    }
}