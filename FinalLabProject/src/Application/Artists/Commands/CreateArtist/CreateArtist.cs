using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Domain.Entities;
using FinalLabProject.Domain.Events.Artist;
using FinalLabProject.Domain.ValueObjects;
using MediatR;

namespace FinalLabProject.Application.Artists.Commands.CreateArtist;

public record CreateArtistCommand : IRequest<int>
{
    public string Name { get; init; } = default!;
    public Username UserName { get; init; } = default!;
    public EmailAddress Email { get; init; } = default!;
    public string Bio { get; init; } = string.Empty;
    public PayoutTier PayoutTier { get; init; } = default!;
    public string PasswordHash { get; init; } = default!;
}

public class CreateArtistCommandHandler : IRequestHandler<CreateArtistCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateArtistCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateArtistCommand request, CancellationToken cancellationToken)
    {
        var entity = new Artist
        {
            Name = request.Name,
            UserName = new Domain.ValueObjects.Username(request.UserName),
            Email = new Domain.ValueObjects.EmailAdress(request.Email),
            Bio = request.Bio,
            PayoutTier = new Domain.ValueObjects.PayoutTier(request.PayoutTier),
            PasswordHash = request.PasswordHash
        };
        if (_context.Artists.Any(a => a.UserName == entity.UserName))
            throw new UserAlreadyExistsException(request.UserName, UserType.Artist);

        entity.AddDomainEvent(new ArtistCreatedEvent(entity));

        _context.Artists.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}