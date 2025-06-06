using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FinalLabProject.Application.Common.Mappings;

namespace FinalLabProject.Application.Listeners.Queries.GetListenersWithPagination;

public record GetListenersWithPaginationQuery : IRequest<PaginatedList<ListenerDto>>
{
  public int PageNumber { get; init; } = 1;
  public int PageSize { get; init; } = 10;
  public string? NameFilter { get; init; }
}

public class GetListenersWithPaginationQueryHandler : IRequestHandler<GetListenersWithPaginationQuery, PaginatedList<ListenerDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListenersWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ListenerDto>> Handle(GetListenersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Listeners.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.NameFilter))
        {
            query = query.Where(l => l.Name.Contains(request.NameFilter));
        }

        return await query
            .OrderBy(l => l.Name)
            .ProjectTo<ListenerDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}