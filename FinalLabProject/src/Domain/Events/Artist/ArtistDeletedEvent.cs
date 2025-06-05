using FinalLabProject.Domain.Common;
using FinalLabProject.Domain.Entities;

namespace FinalLabProject.Domain.Events.Artist;

public class ArtistDeletedEvent : EntityEvent<Artist>
{
    public ArtistDeletedEvent(Artist artist)
        : base(artist, EntityEventType.Deleted)
    {
    }
}