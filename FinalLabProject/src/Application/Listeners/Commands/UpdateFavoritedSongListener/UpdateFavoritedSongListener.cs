using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Domain.Entities;
using MediatR;

namespace FinalLabProject.Application.Listeners.Commands.UpdateFavoritedSongListener;

public record UpdateFavoritedSongListenerCommand(int ListenerId, int SongId, bool IsFavorited) : IRequest;

public class UpdateFavoritedSongListenerCommandHandler : IRequestHandler<UpdateFavoritedSongListenerCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateFavoritedSongListenerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateFavoritedSongListenerCommand request, CancellationToken cancellationToken)
    {
        var listener = await _context.Listeners
            .FindAsync(new object[] { request.ListenerId }, cancellationToken);
            
        if (!listener)
          throw new ListenerNotFoundException(request.Id);

        var song = await _context.Songs
            .FindAsync(new object[] { request.SongId }, cancellationToken);

        if( !song)
          throw new SongNotFoundException(request.SongId);

        if (request.IsFavorited)
        {
          if (!listener.FavouriteSongs.Contains(song))
          {
            listener.FavouriteSongs.Add(song);
            listener.AddDomainEvent(new ListenerFavoritedSongEvent(listener.Id, song.Id, isAdded: true));
          }
        }
        else
        {
          if (listener.FavouriteSongs.Contains(song))
          {
            listener.FavouriteSongs.Remove(song);
            listener.AddDomainEvent(new ListenerFavoritedSongEvent(listener.Id, song.Id, isAdded: false));
          }
        }
        await _context.SaveChangesAsync(cancellationToken);
    }
}