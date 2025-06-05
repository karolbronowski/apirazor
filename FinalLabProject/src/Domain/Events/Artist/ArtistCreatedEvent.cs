﻿using MediatR;

namespace FinalLabProject.Domain.Events.Artist;

public class ArtistCreatedEvent : BaseEvent
{
    public int ArtistId { get; }

    public ArtistCreatedEvent(int artistId)
    {
        ArtistId = artistId;
    }
}