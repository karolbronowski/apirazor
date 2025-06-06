using FinalLabProject.Domain.Entities;

namespace FinalLabProject.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<Artist> Artists { get; }

    DbSet<Listener> Listeners { get; }

    DbSet<Song> Songs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
