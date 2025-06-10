using Microsoft.AspNetCore.Identity;
using FinalLabProject.Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalLabProject.WebAPI.Endpoints;

public class ListUsers : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(this)
            .RequireAuthorization();

        group.MapGet("/all", GetAllUsers).AllowAnonymous();
    }

    public static IEnumerable<object> GetAllUsers([FromServices] UserManager<ApplicationUser> userManager)
        => userManager.Users.Select(u => new { u.Id, u.UserName, u.Email, u.EmailConfirmed });
}