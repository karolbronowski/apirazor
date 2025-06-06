using System;

namespace FinalLabProject.Domain.Exceptions.ArtistExceptions;

public class InvalidEmailException : Exception
{
    public InvalidEmailException(string email)
        : base($"The email '{email}' is not valid.")
    {
    }
}