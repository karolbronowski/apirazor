using System;

namespace FinalLabProject.Domain.Exceptions.Listener;

public class ListenerNotFoundException : Exception
{
    public ListenerNotFoundException(int listenerId)
        : base($"Listener with ID {listenerId} was not found.")
    {
    }
}