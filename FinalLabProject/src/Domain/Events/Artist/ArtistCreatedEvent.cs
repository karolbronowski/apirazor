﻿using FinalLabProject.Domain.Common;
using FinalLabProject.Domain.Entities;

namespace FinalLabProject.Domain.Events.Artist;

public class ArtistCreatedEvent : EntityEvent<Artist>
{
    public ArtistCreatedEvent(Artist artist)
        : base(artist, EntityEventType.Created)
    {
    }
}