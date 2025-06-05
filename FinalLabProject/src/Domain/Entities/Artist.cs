using FinalLabProject.Domain.ValueObjects;

namespace FinalLabProject.Domain.Entities;

public class Artist : UserAccount
{
    public string Bio { get; set; } = string.Empty;
    public PayoutTier PayoutTier { get; set; } = null!;
    public ICollection<Song> Songs { get; set; } = new List<Song>();
}