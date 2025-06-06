using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Application.Common.Models;
using FinalLabProject.Application.Common.Mappings;
using MediatR;

namespace FinalLabProject.Application.Songs.Queries.GetArtistSongsWithPagination;

public record GetArtistSongsWithPaginationQuery : IRequest<PaginatedList<SongDto>>
{
    public int ArtistId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetArtistSongsWithPaginationQueryHandler : IRequestHandler<GetArtistSongsWithPaginationQuery, PaginatedList<SongDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetArtistSongsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<SongDto>> Handle(GetArtistSongsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Songs
            .Where(x => x.ArtistId == request.ArtistId)
            .OrderBy(x => x.Title)
            .ProjectTo<SongDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}