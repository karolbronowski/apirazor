using System;

namespace FinalLabProject.Domain.Exceptions.Common;

public class InvalidUsernameException : Exception
{
    public InvalidUsernameException(string username, string reason)
        : base($"Username '{username}' is invalid: {reason}")
    {
    }
}