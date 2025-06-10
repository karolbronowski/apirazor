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
        var group = app.MapGroup(this)
            .RequireAuthorization();

        group.MapPost("/", CreateListener).AllowAnonymous();
        group.MapPut(UpdateListener, "{id}");
        group.MapPut(UpdateFavoritedSongListener, "favorite-song");
        group.MapDelete(DeleteListener, "{id}");
        group.MapGet(GetListenersWithPagination);
        group.MapGet(GetListenerByUsername, "by-username");
    }

    /// <summary>
    /// Creates a new listener account.
    /// </summary>
    /// <param name="sender">The mediator sender instance.</param>
    /// <param name="command">
    /// The command containing listener creation data.
    /// <para>
    /// <b>Validation rules (see <c>CreateListenerCommandValidator</c>):</b>
    /// <list type="bullet">
    /// <item><description>Name: Required, max 100 chars</description></item>
    /// <item><description>Username: Required</description></item>
    /// <item><description>Email: Required, valid email format</description></item>
    /// <item><description>Password: Required if UserId is not provided, min 4 chars</description></item>
    /// <item><description>UserId: Must be null if Password is provided</description></item>
    /// </list>
    /// </para>
    /// </param>
    /// <returns>The ID of the newly created listener.</returns>
    public Task<int> CreateListener(ISender sender, CreateListenerCommand command)
        => sender.Send(command);

    /// <summary>
    /// Updates an existing listener's information.
    /// </summary>
    /// <param name="sender">The mediator sender instance.</param>
    /// <param name="id">The ID of the listener to update.</param>
    /// <param name="command">
    /// The command containing updated listener data. The ID in the command must match the route ID.
    /// <para>
    /// <b>Validation rules (see <c>UpdateListenerCommandValidator</c>):</b>
    /// <list type="bullet">
    /// <item><description>Id: Required, must be greater than 0</description></item>
    /// <item><description>Name: Max 100 chars (optional)</description></item>
    /// </list>
    /// </para>
    /// </param>
    /// <returns>No content if successful; BadRequest if IDs do not match.</returns>
    public async Task<IResult> UpdateListener(ISender sender, int id, UpdateListenerCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    /// <summary>
    /// Updates the favorited song for a specific listener.
    /// </summary>
    /// <param name="sender">The mediator sender instance.</param>
    /// <param name="command">
    /// The command containing the listener and song IDs and the favorite status.
    /// <para>
    /// <b>Validation rules (see <c>UpdateFavoritedSongListenerCommandValidator</c>):</b>
    /// <list type="bullet">
    /// <item><description>ListenerId: Required, must be greater than 0</description></item>
    /// <item><description>SongId: Required, must be greater than 0</description></item>
    /// </list>
    /// </para>
    /// </param>
    /// <returns>No content if successful.</returns>
    public async Task<IResult> UpdateFavoritedSongListener(ISender sender, UpdateFavoritedSongListenerCommand command)
    {
        await sender.Send(command);
        return Results.NoContent();
    }

    /// <summary>
    /// Deletes a listener by ID.
    /// </summary>
    /// <param name="sender">The mediator sender instance.</param>
    /// <param name="id">The ID of the listener to delete.</param>
    /// <returns>No content if successful.</returns>
    public async Task<IResult> DeleteListener(ISender sender, int id)
    {
        await sender.Send(new DeleteListenerCommand(id));
        return Results.NoContent();
    }

    /// <summary>
    /// Retrieves a paginated list of listeners.
    /// </summary>
    /// <param name="sender">The mediator sender instance.</param>
    /// <param name="query">The pagination and filter query parameters.</param>
    /// <returns>A paginated list of listener DTOs.</returns>
    public async Task<PaginatedList<ListenerDto>> GetListenersWithPagination(ISender sender, [AsParameters] GetListenersWithPaginationQuery query)
        => await sender.Send(query);

    /// <summary>
    /// Retrieves a listener by their username.
    /// </summary>
    /// <param name="sender">The mediator sender instance.</param>
    /// <param name="query">The query containing the listener username to search for.</param>
    /// <returns>The listener DTO if found; otherwise, null.</returns>
    public async Task<ListenerDto?> GetListenerByUsername(ISender sender, [AsParameters] GetListenerByUsernameQuery query)
        => await sender.Send(query);
}