using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Domain.Entities;
using MediatR;

namespace FinalLabProject.Application.Listeners.Commands.UpdateListener;

public record UpdateListenerCommand : IRequest
{
    public int Id { get; init; }
    public string? Name { get; init; }
}

public class UpdateListenerCommandHandler : IRequestHandler<UpdateListenerCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateListenerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateListenerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Listeners.FindAsync(new object[] { request.Id }, cancellationToken);

        if (!entity)
            throw new ListenerNotFoundException(request.Id);

        if (request.Name is not null)
            entity.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);
    }
}