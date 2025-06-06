using FinalLabProject.Domain.ValueObjects;

namespace FinalLabProject.Application.Artists.Queries;

public class ArtistDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Bio { get; set; } = default!;
    public string PayoutTier { get; set; } = default!;
}