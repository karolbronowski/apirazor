using MediatR;
using Microsoft.EntityFrameworkCore;
using FinalLabProject.Application.Common.Interfaces;
namespace FinalLabProject.Application.Listeners.Queries.GetListenerById;

public record GetListenerByIdQuery(int Id) : IRequest<ListenerDto>;

public class GetListenerByIdQueryHandler : IRequestHandler<GetListenerByIdQuery, ListenerDto?>
{
    private readonly IApplicationDbContext _context;

    public GetListenerByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ListenerDto?> Handle(GetListenerByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Listeners
            .AsNoTracking()
            .Where(l => l.Id == request.Id)
            .Select(l => new ListenerDto
            {
                Id = l.Id,
                Name = l.Name,
                Username = l.Username.Value
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}