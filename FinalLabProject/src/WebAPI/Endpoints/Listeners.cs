using FinalLabProject.Application.Listeners.Commands.CreateListener;
using FinalLabProject.Application.Listeners.Commands.UpdateListener;
using FinalLabProject.Application.Listeners.Commands.UpdateFavoritedSongListener;
using FinalLabProject.Application.Listeners.Commands.DeleteListener;
using FinalLabProject.Application.Listeners.Queries.GetListenersWithPagination;
using FinalLabProject.Application.Listeners.Queries.GetListenerByUsername;
using FinalLabProject.Application.Common.Models;
using FinalLabProject.Application.Listeners.Queries;
using MediatR;

namespace FinalLabProject.WebAPI.Endpoints;

public class Listeners : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateListener)
            .MapPut(UpdateListener, "{id}")
            .MapPut(UpdateFavoritedSongListener, "favorite-song")
            .MapDelete(DeleteListener, "{id}")
            .MapGet(GetListenersWithPagination)
            .MapGet(GetListenerByUsername, "by-username");
    }

    public Task<int> CreateListener(ISender sender, CreateListenerCommand command)
        => sender.Send(command);

    public async Task<IResult> UpdateListener(ISender sender, int id, UpdateListenerCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> UpdateFavoritedSongListener(ISender sender, UpdateFavoritedSongListenerCommand command)
    {
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteListener(ISender sender, int id)
    {
        await sender.Send(new DeleteListenerCommand(id));
        return Results.NoContent();
    }

    public async Task<PaginatedList<ListenerDto>> GetListenersWithPagination(ISender sender, [AsParameters] GetListenersWithPaginationQuery query)
        => await sender.Send(query);

    public async Task<ListenerDto?> GetListenerByUsername(ISender sender, [AsParameters] GetListenerByUsernameQuery query)
        => await sender.Send(query);
}