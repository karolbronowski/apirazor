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
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateArtist)
            .MapPut(UpdateArtist, "{id}")
            .MapPut(UpdateArtistPayoutTier, "payout-tier/{id}")
            .MapDelete(DeleteArtist, "{id}")
            .MapGet(GetArtistById, "{id}")
            .MapGet(GetArtistsWithPagination)
            .MapGet(GetArtistByName, "by-name");
    }

    public Task<int> CreateArtist(ISender sender, CreateArtistCommand command)
        => sender.Send(command);

    public async Task<IResult> UpdateArtist(ISender sender, int id, UpdateArtistCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> UpdateArtistPayoutTier(ISender sender, int id, UpdateArtistPayoutTierCommand command)
    {
        if (id != command.ArtistId) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteArtist(ISender sender, int id)
    {
        await sender.Send(new DeleteArtistCommand(id));
        return Results.NoContent();
    }

    public async Task<ArtistDto?> GetArtistById(ISender sender, int id)
        => await sender.Send(new GetArtistByIdQuery(id));

    public async Task<PaginatedList<ArtistDto>> GetArtistsWithPagination(ISender sender, [AsParameters] GetArtistsWithPaginationQuery query)
        => await sender.Send(query);

    public async Task<List<ArtistDto>> GetArtistByName(ISender sender, [AsParameters] GetArtistByNameQuery query)
        => await sender.Send(query);
}