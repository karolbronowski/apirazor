using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Application.Common.Models;
using FinalLabProject.Application.Common.Mappings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalLabProject.Application.Artists.Queries.GetArtistsWithPagination;

public record GetArtistsWithPaginationQuery : IRequest<PaginatedList<ArtistDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? NameFilter { get; init; }
}

public class GetArtistsWithPaginationQueryHandler : IRequestHandler<GetArtistsWithPaginationQuery, PaginatedList<ArtistDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetArtistsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ArtistDto>> Handle(GetArtistsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Artists.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.NameFilter))
        {
            query = query.Where(a => a.Name.Contains(request.NameFilter));
        }

        return await query
            .OrderBy(a => a.Name)
            .ProjectTo<ArtistDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}