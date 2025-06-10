using MediatR;
using Microsoft.EntityFrameworkCore;
using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Application.Artists.Queries;

namespace FinalLabProject.Application.Artists.Queries.GetArtistByEmail;

public record GetArtistByEmailQuery(string Email) : IRequest<ArtistDto?>;

public class GetArtistByEmailQueryHandler : IRequestHandler<GetArtistByEmailQuery, ArtistDto?>
{
    private readonly IApplicationDbContext _context;

    public GetArtistByEmailQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ArtistDto?> Handle(GetArtistByEmailQuery request, CancellationToken cancellationToken)
    {
        return await _context.Artists
            .AsNoTracking()
            .Where(a => a.Email.Value == request.Email)
            .Select(a => new ArtistDto
            {
                Id = a.Id,
                Name = a.Name,
                Username = a.Username.Value,
                Bio = a.Bio,
                PayoutTier = a.PayoutTier.Name,
                Email = a.Email.Value
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}