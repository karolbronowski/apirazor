using FinalLabProject.Application.Songs.Commands.CreateSong;
using FinalLabProject.Application.Songs.Commands.UpdateSong;
using FinalLabProject.Application.Songs.Commands.PlaySong;
using FinalLabProject.Application.Songs.Commands.DeleteSong;
using FinalLabProject.Application.Songs.Queries.GetSongsWithPagination;
using FinalLabProject.Application.Songs.Queries.GetArtistSongsWithPagination;
using FinalLabProject.Application.Songs.Queries;
using FinalLabProject.Application.Common.Models;
using MediatR;

namespace FinalLabProject.WebAPI.Endpoints;

public class Songs : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateSong)
            .MapPut(UpdateSong, "{id}")
            .MapPut(PlaySong, "play/{songId}")
            .MapDelete(DeleteSong, "{id}")
            .MapGet(GetSongsWithPagination)
            .MapGet(GetArtistSongsWithPagination, "by-artist");
    }

    /// <summary>
    /// Creates a new song.
    /// </summary>
    /// <param name="sender">The mediator sender instance.</param>
    /// <param name="command">
    /// The command containing song creation data.
    /// <para>
    /// <b>Validation rules (see <c>CreateSongCommandValidator</c>):</b>
    /// <list type="bullet">
    /// <item><description>Title: Required, max 200 chars</description></item>
    /// <item><description>ArtistId: Required, must be greater than 0</description></item>
    /// </list>
    /// </para>
    /// </param>
    /// <returns>The ID of the newly created song.</returns>
    public Task<int> CreateSong(ISender sender, CreateSongCommand command)
        => sender.Send(command);

    /// <summary>
    /// Updates an existing song's information.
    /// </summary>
    /// <param name="sender">The mediator sender instance.</param>
    /// <param name="id">The ID of the song to update.</param>
    /// <param name="command">
    /// The command containing updated song data. The ID in the command must match the route ID.
    /// <para>
    /// <b>Validation rules (see <c>UpdateSongCommandValidator</c>):</b>
    /// <list type="bullet">
    /// <item><description>Title: Max 200 chars (optional)</description></item>
    /// <item><description>ArtistId: Must be greater than 0 if provided</description></item>
    /// </list>
    /// </para>
    /// </param>
    /// <returns>No content if successful; BadRequest if IDs do not match.</returns>
    public async Task<IResult> UpdateSong(ISender sender, int id, UpdateSongCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    /// <summary>
    /// Increments the play count for a song.
    /// </summary>
    /// <param name="sender">The mediator sender instance.</param>
    /// <param name="songId">The ID of the song to play.</param>
    /// <para>
    /// <b>Validation rules (see <c>PlaySongCommandValidator</c>):</b>
    /// <list type="bullet">
    /// <item><description>SongId: Required, must be greater than 0</description></item>
    /// </list>
    /// </para>
    /// <returns>No content if successful.</returns>
    public async Task<IResult> PlaySong(ISender sender, int songId)
    {
        await sender.Send(new PlaySongCommand(songId));
        return Results.NoContent();
    }

    /// <summary>
    /// Deletes a song by ID.
    /// </summary>
    /// <param name="sender">The mediator sender instance.</param>
    /// <param name="id">The ID of the song to delete.</param>
    /// <returns>No content if successful.</returns>
    public async Task<IResult> DeleteSong(ISender sender, int id)
    {
        await sender.Send(new DeleteSongCommand(id));
        return Results.NoContent();
    }

    /// <summary>
    /// Retrieves a paginated list of songs.
    /// </summary>
    /// <param name="sender">The mediator sender instance.</param>
    /// <param name="query">The pagination and filter query parameters.</param>
    /// <returns>A paginated list of song DTOs.</returns>
    public async Task<PaginatedList<SongDto>> GetSongsWithPagination(ISender sender, [AsParameters] GetSongsWithPaginationQuery query)
        => await sender.Send(query);

    /// <summary>
    /// Retrieves a paginated list of songs for a specific artist.
    /// </summary>
    /// <param name="sender">The mediator sender instance.</param>
    /// <param name="query">The query containing the artist ID and pagination parameters.</param>
    /// <returns>A paginated list of song DTOs for the specified artist.</returns>
    public async Task<PaginatedList<SongDto>> GetArtistSongsWithPagination(ISender sender, [AsParameters] GetArtistSongsWithPaginationQuery query)
        => await sender.Send(query);
}