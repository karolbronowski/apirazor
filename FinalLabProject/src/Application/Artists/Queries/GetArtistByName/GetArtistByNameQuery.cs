using MediatR;
using Microsoft.EntityFrameworkCore;
using FinalLabProject.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FinalLabProject.Application.Artists.Queries.GetArtistByName;

public record GetArtistByNameQuery(string Name) : IRequest<List<ArtistDto>>;

public class GetArtistByNameQueryHandler : IRequestHandler<GetArtistByNameQuery, List<ArtistDto>>
{
    private readonly IApplicationDbContext _context;

    public GetArtistByNameQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ArtistDto>> Handle(GetArtistByNameQuery request, CancellationToken cancellationToken)
    {
        return await _context.Artists
            .AsNoTracking()
            .Where(a => a.Name.Contains(request.Name))
            .Select(a => new ArtistDto
            {
                Id = a.Id,
                Name = a.Name,
                UserName = a.UserName.Value,
                Bio = a.Bio,
                PayoutTier = a.PayoutTier.Value
            })
            .ToListAsync(cancellationToken);
    }
}