using MediatR;
using Microsoft.EntityFrameworkCore;
using FinalLabProject.Application.Common.Interfaces;

namespace FinalLabProject.Application.Listeners.Queries.GetListenerByUserName;

public record GetListenerByUserNameQuery(string UserName) : IRequest<ListenerDto>;

public class GetListenerByUserNameQueryHandler : IRequestHandler<GetListenerByUserNameQuery, ListenerDto?>
{
    private readonly IApplicationDbContext _context;

    public GetListenerByUserNameQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ListenerDto?> Handle(GetListenerByUserNameQuery request, CancellationToken cancellationToken)
    {
        return await _context.Listeners
            .AsNoTracking()
            .Where(l => l.UserName.Value == request.UserName)
            .Select(l => new ListenerDto
            {
                Id = l.Id,
                Name = l.Name,
                UserName = l.UserName.Value
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}