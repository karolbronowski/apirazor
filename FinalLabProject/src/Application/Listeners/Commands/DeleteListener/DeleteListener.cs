using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Domain.Entities;
using FinalLabProject.Domain.Events.Listener;
using MediatR;

namespace FinalLabProject.Application.Listeners.Commands.DeleteListener;

public record DeleteListenerCommand(int Id) : IRequest;

public class DeleteListenerCommandHandler : IRequestHandler<DeleteListenerCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteListenerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteListenerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Listeners.FindAsync(new object[] { request.Id }, cancellationToken);
        if (!entity)
            throw new ListenerNotFoundException(request.Id);

        _context.Listeners.Remove(entity);

        entity.AddDomainEvent(new ListenerDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}