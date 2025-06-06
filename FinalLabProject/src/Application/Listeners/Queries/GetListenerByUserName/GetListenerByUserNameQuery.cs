using MediatR;
using Microsoft.EntityFrameworkCore;
using FinalLabProject.Application.Common.Interfaces;

namespace FinalLabProject.Application.Listeners.Queries.GetListenerByUsername;

public record GetListenerByUsernameQuery(string Username) : IRequest<ListenerDto>;

public class GetListenerByUsernameQueryHandler : IRequestHandler<GetListenerByUsernameQuery, ListenerDto?>
{
    private readonly IApplicationDbContext _context;

    public GetListenerByUsernameQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ListenerDto?> Handle(GetListenerByUsernameQuery request, CancellationToken cancellationToken)
    {
        return await _context.Listeners
            .AsNoTracking()
            .Where(l => l.Username.Value == request.Username)
            .Select(l => new ListenerDto
            {
                Id = l.Id,
                Name = l.Name,
                Username = l.Username.Value
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}