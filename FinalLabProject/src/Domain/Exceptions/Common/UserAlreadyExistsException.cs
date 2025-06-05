using FinalLabProject.Domain.Enums;

namespace FinalLabProject.Domain.Exceptions.Common;

public class UserAlreadyExistsException : Exception
{
    public string Email { get; }
    public UserType Type { get; }

    public UserAlreadyExistsException(string email, UserType type = UserType.User)
        : base(GenerateMessage(email, type))
    {
        Email = email;
        Type = type;
    }

    private static string GenerateMessage(string email, UserType type)
    {
        return type switch
        {
            UserType.Artist => $"An artist with email '{email}' already exists.",
            UserType.Listener => $"A listener with email '{email}' already exists.",
            _ => $"A user with email '{email}' already exists."
        };
    }
}