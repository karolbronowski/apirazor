using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Domain.Entities;
using FinalLabProject.Domain.Events.ListenerEvents;
using FinalLabProject.Domain.Exceptions.Common;
using FinalLabProject.Domain.ValueObjects;
using FinalLabProject.Domain.Enums;
using MediatR;

namespace FinalLabProject.Application.Listeners.Commands.CreateListener;

public record CreateListenerCommand : IRequest<int>
{
    public string? UserId { get; init; } = default!;
    public string Name { get; init; } = default!;
    public string Username { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string? Password { get; init; } = default!;
}

public class CreateListenerCommandHandler : IRequestHandler<CreateListenerCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;

    public CreateListenerCommandHandler(IApplicationDbContext context, IIdentityService identityService)
    {
        _context = context;
        _identityService = identityService;
    }

    public async Task<int> Handle(CreateListenerCommand request, CancellationToken cancellationToken)
    {
        if (_context.Listeners.Any(l => l.Email.Value == request.Email) ||
            _context.Artists.Any(a => a.Email.Value == request.Email))
        {
            throw new UserAlreadyExistsException(request.Email, UserType.Listener);
        }

        string userId;
        if (string.IsNullOrEmpty(request.UserId))
        {
            var (result, createdUserId) = await _identityService.CreateUserAsync(request.Username,  request.Email, request.Password!);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors));
            userId = createdUserId;
        }
        else
        {
            userId = request.UserId;
        }

        var entity = new Listener
        {
            UserId = userId,
            Name = request.Name,
            Username = new Domain.ValueObjects.Username(request.Username),
            Email = new Domain.ValueObjects.EmailAddress(request.Email),
        };

        entity.AddDomainEvent(new ListenerCreatedEvent(entity));

        _context.Listeners.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}