using System;

namespace FinalLabProject.Domain.Exceptions.SongExceptions;

public class SongNotFoundException : Exception
{
    public SongNotFoundException(int songId)
        : base($"Song with ID {songId} was not found.")
    {
    }
}