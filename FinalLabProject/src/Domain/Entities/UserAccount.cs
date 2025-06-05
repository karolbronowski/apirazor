using FinalLabProject.Domain.ValueObjects;

namespace FinalLabProject.Domain.Entities;

// Remains in Entities, as it's a DB entity, that's why i didn't move it to Common folder.
public abstract class UserAccount : BaseAuditableEntity
{
    public string Name { get; set; } = default!;
    public Username Username { get; set; } = default!;
    public EmailAddress Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
}