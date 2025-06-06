using System;

namespace FinalLabProject.Domain.Exceptions.ArtistExceptions;

public class ArtistNotFoundException : Exception
{
    public ArtistNotFoundException(int artistId)
        : base($"Artist with ID {artistId} was not found.")
    {
    }
}