using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Application.Common.Models;
using FinalLabProject.Domain.Entities;
using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace FinalLabProject.Application.Songs.Queries.GetSongsWithPagination;

public record GetSongsWithPaginationQuery : IRequest<PaginatedList<SongDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetSongsWithPaginationQueryHandler : IRequestHandler<GetSongsWithPaginationQuery, PaginatedList<SongDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSongsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<SongDto>> Handle(GetSongsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Songs
            .OrderBy(x => x.Title)
            .ProjectTo<SongDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}