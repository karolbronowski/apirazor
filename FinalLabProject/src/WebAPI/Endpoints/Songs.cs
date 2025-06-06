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

    public Task<int> CreateSong(ISender sender, CreateSongCommand command)
        => sender.Send(command);

    public async Task<IResult> UpdateSong(ISender sender, int id, UpdateSongCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> PlaySong(ISender sender, int songId)
    {
        await sender.Send(new PlaySongCommand(songId));
        return Results.NoContent();
    }

    public async Task<IResult> DeleteSong(ISender sender, int id)
    {
        await sender.Send(new DeleteSongCommand(id));
        return Results.NoContent();
    }

    public async Task<PaginatedList<SongDto>> GetSongsWithPagination(ISender sender, [AsParameters] GetSongsWithPaginationQuery query)
        => await sender.Send(query);

    public async Task<PaginatedList<SongDto>> GetArtistSongsWithPagination(ISender sender, [AsParameters] GetArtistSongsWithPaginationQuery query)
        => await sender.Send(query);
}