using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Domain.Entities;
using FinalLabProject.Domain.Events.ArtistEvents;
using FinalLabProject.Domain.Exceptions.Common;
using FinalLabProject.Domain.ValueObjects;
using FinalLabProject.Domain.Enums;
using MediatR;

namespace FinalLabProject.Application.Artists.Commands.CreateArtist;

public record CreateArtistCommand : IRequest<int>
{
    public string UserId { get; init; } = default!;
    public string Name { get; init; } = default!;
    public string Username { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string Bio { get; init; } = string.Empty;
    public string PayoutTier { get; init; } = default!;
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
            UserId = request.UserId,
            Name = request.Name,
            Username = new Domain.ValueObjects.Username(request.Username),
            Email = new Domain.ValueObjects.EmailAddress(request.Email),
            Bio = request.Bio,
            PayoutTier = new Domain.ValueObjects.PayoutTier(request.PayoutTier),
        };
        if (_context.Artists.Any(a => a.Username == entity.Username))
            throw new UserAlreadyExistsException(request.Username, UserType.Artist);

        entity.AddDomainEvent(new ArtistCreatedEvent(entity));

        _context.Artists.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}