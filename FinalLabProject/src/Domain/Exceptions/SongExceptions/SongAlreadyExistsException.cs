using System;

namespace FinalLabProject.Domain.Exceptions.SongExceptions;

public class SongAlreadyExistsException : Exception
{
    public SongAlreadyExistsException(string title, int artistId)
        : base($"A song with title '{title}' already exists for artist with ID {artistId}.")
    {
    }
}