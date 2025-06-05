using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Domain.Entities;
using FinalLabProject.Domain.Events.Listener;
using FinalLabProject.Domain.ValueObjects;
using MediatR;

namespace FinalLabProject.Application.Listeners.Commands.CreateListener;

public record CreateListenerCommand : IRequest<int>
{
    public string Name { get; init; } = default!;
    public Username UserName { get; init; } = default!;
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
            Name = request.Name,
            UserName = new Username(request.UserName)
        };

        if (_context.Listeners.Any(l => l.UserName == request.UserName))
            throw new UserAlreadyExistsException(request.UserName, UserType.Listener);
            
        entity.AddDomainEvent(new ListenerCreatedEvent(entity));

        _context.Listeners.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}