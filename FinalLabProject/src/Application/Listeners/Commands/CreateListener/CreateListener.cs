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
    public string UserId { get; init; } = default!;
    public string Name { get; init; } = default!;
    public string Username { get; init; } = default!;
    public string Email { get; init; } = default!;
}

public class CreateListenerCommandHandler : IRequestHandler<CreateListenerCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateListenerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateListenerCommand request, CancellationToken cancellationToken)
    {
        var entity = new Listener
        {
            UserId = request.UserId,
            Name = request.Name,
            Username = new Username(request.Username),
            Email = new EmailAddress(request.Email),
        };

        if (_context.Listeners.Any(l => l.Username == request.Username))
            throw new UserAlreadyExistsException(request.Username, UserType.Listener);
            
        entity.AddDomainEvent(new ListenerCreatedEvent(entity));

        _context.Listeners.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}