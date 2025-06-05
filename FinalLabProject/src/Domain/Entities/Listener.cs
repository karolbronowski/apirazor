using FinalLabProject.Domain.ValueObjects;

namespace FinalLabProject.Domain.Entities;

public class Listener : UserAccount
{
    public ICollection<Song> FavouriteSongs { get; set; } = new List<Song>();
}