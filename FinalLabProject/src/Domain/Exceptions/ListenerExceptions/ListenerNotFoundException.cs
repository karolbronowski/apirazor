using System;

namespace FinalLabProject.Domain.Exceptions.ListenerExceptions;

public class ListenerNotFoundException : Exception
{
    public ListenerNotFoundException(int listenerId)
        : base($"Listener with ID {listenerId} was not found.")
    {
    }
}