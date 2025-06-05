using FinalLabProject.Domain.Entities;

namespace FinalLabProject.Domain.Entities;

public class Song : BaseAuditableEntity
{
    public string Title { get; set; } = default!;
    public int ArtistId { get; set; }
    public int ListenedTimes { get; set; }
    public Artist Artist { get; set; } = null!;
}