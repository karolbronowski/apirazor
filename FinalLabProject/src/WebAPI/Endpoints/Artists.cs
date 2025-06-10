using FinalLabProject.Application.Artists.Commands.CreateArtist;
using FinalLabProject.Application.Artists.Commands.UpdateArtist;
using FinalLabProject.Application.Artists.Commands.UpdateArtistPayoutTier;
using FinalLabProject.Application.Artists.Commands.DeleteArtist;
using FinalLabProject.Application.Artists.Queries.GetArtistById;
using FinalLabProject.Application.Artists.Queries.GetArtistByName;
using FinalLabProject.Application.Artists.Queries.GetArtistsWithPagination;
using FinalLabProject.Application.Artists.Queries;
using FinalLabProject.Application.Common.Models;
using MediatR;

namespace FinalLabProject.WebAPI.Endpoints;

public class Artists : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(this)
            .RequireAuthorization();

        group.MapPost("/", CreateArtist).AllowAnonymous();
        group.MapPut(UpdateArtist, "{id}");
        group.MapPut(UpdateArtistPayoutTier, "payout-tier/{id}");
        group.MapDelete(DeleteArtist, "{id}");
        group.MapGet(GetArtistById, "{id}");
        group.MapGet(GetArtistsWithPagination);
        group.MapGet(GetArtistByName, "by-name");
    }

    /// <summary>
    /// Creates a new artist account.
    /// </summary>
    /// <param name="sender">The mediator sender instance.</param>
    /// <param name="command">
    /// The command containing artist creation data.
    /// <para>
    /// <b>Validation rules (see <c>CreateArtistCommandValidator</c>):</b>
    /// <list type="bullet">
    /// <item><description>Name: Required, max 100 chars</description></item>
    /// <item><description>Username: Required</description></item>
    /// <item><description>Email: Required, valid email format</description></item>
    /// <item><description>Password: Required if UserId is not provided, min 4 chars</description></item>
    /// <item><description>UserId: Must be null if Password is provided</description></item>
    /// <item><description>Bio: Max 500 chars</description></item>
    /// <item><description>PayoutTier: Required</description></item>
    /// </list>
    /// </para>
    /// </param>
    /// <returns>The ID of the newly created artist.</returns>
    public Task<int> CreateArtist(ISender sender, CreateArtistCommand command)
        => sender.Send(command);

    /// <summary>
    /// Updates an existing artist's information.
    /// </summary>
    /// <param name="sender">The mediator sender instance.</param>
    /// <param name="id">The ID of the artist to update.</param>
    /// <param name="command">
    /// The command containing updated artist data. The ID in the command must match the route ID.
    /// <para>
    /// <b>Validation rules (see <c>UpdateArtistCommandValidator</c>):</b>
    /// <list type="bullet">
    /// <item><description>Id: Required, must be greater than 0</description></item>
    /// <item><description>Name: Max 100 chars (optional)</description></item>
    /// <item><description>Bio: Max 500 chars (optional)</description></item>
    /// </list>
    /// </para>
    /// </param>
    /// <returns>No content if successful; BadRequest if IDs do not match.</returns>
    public async Task<IResult> UpdateArtist(ISender sender, int id, UpdateArtistCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    /// <summary>
    /// Updates the payout tier for a specific artist.
    /// </summary>
    /// <param name="sender">The mediator sender instance.</param>
    /// <param name="id">The ID of the artist whose payout tier is being updated.</param>
    /// <param name="command">
    /// The command containing the new payout tier. The ArtistId in the command must match the route ID.
    /// <para>
    /// <b>Validation rules (see <c>UpdateArtistPayoutTierCommandValidator</c>):</b>
    /// <list type="bullet">
    /// <item><description>ArtistId: Required, must be greater than 0</description></item>
    /// <item><description>PayoutTier: Required</description></item>
    /// </list>
    /// </para>
    /// </param>
    /// <returns>No content if successful; BadRequest if IDs do not match.</returns>
    public async Task<IResult> UpdateArtistPayoutTier(ISender sender, int id, UpdateArtistPayoutTierCommand command)
    {
        if (id != command.ArtistId) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    /// <summary>
    /// Deletes an artist by ID.
    /// </summary>
    /// <param name="sender">The mediator sender instance.</param>
    /// <param name="id">The ID of the artist to delete.</param>
    /// <returns>No content if successful.</returns>
    public async Task<IResult> DeleteArtist(ISender sender, int id)
    {
        await sender.Send(new DeleteArtistCommand(id));
        return Results.NoContent();
    }

    /// <summary>
    /// Retrieves an artist by their unique ID.
    /// </summary>
    /// <param name="sender">The mediator sender instance.</param>
    /// <param name="id">The ID of the artist to retrieve.</param>
    /// <returns>The artist DTO if found; otherwise, null.</returns>
    public async Task<ArtistDto?> GetArtistById(ISender sender, int id)
        => await sender.Send(new GetArtistByIdQuery(id));

    /// <summary>
    /// Retrieves a paginated list of artists.
    /// </summary>
    /// <param name="sender">The mediator sender instance.</param>
    /// <param name="query">The pagination and filter query parameters.</param>
    /// <returns>A paginated list of artist DTOs.</returns>
    public async Task<PaginatedList<ArtistDto>> GetArtistsWithPagination(ISender sender, [AsParameters] GetArtistsWithPaginationQuery query)
        => await sender.Send(query);

    /// <summary>
    /// Retrieves artists by their name.
    /// </summary>
    /// <param name="sender">The mediator sender instance.</param>
    /// <param name="query">The query containing the artist name to search for.</param>
    /// <returns>A list of artist DTOs matching the given name.</returns>
    public async Task<List<ArtistDto>> GetArtistByName(ISender sender, [AsParameters] GetArtistByNameQuery query)
        => await sender.Send(query);
}