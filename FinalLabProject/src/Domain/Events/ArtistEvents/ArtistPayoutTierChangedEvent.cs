using FinalLabProject.Domain.Common;
using FinalLabProject.Domain.Entities;
using FinalLabProject.Domain.ValueObjects;

namespace FinalLabProject.Domain.Events.ArtistEvents;

public class ArtistPayoutTierChangedEvent : BaseEvent
{
    public Artist Artist { get; }
    public PayoutTier OldPayoutTier { get; }
    public PayoutTier NewPayoutTier { get; }

    public ArtistPayoutTierChangedEvent(Artist artist, PayoutTier oldPayoutTier, PayoutTier newPayoutTier)
    {
        Artist = artist;
        OldPayoutTier = oldPayoutTier;
        NewPayoutTier = newPayoutTier;
    }
}