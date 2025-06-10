using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FinalLabProject.Domain.Enums;

namespace FinalLabProject.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string email, string password, UserType userType)
    {
        var user = new ApplicationUser
        {
            // user manager expected UserName to be the same as Email... it's impossible to login then, so yeah whole UserName value object proved to be complete waste of time :')
            UserName = email,
            Email = email,
        };

        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);
            await _userManager.AddToRoleAsync(user, userType.ToString());
        }
        else
        {
            throw new Exception(string.Join(", ", result.Errors));
        }

        return (result.ToApplicationResult(), user.Id);
    }

/*************  ✨ Windsurf Command ⭐  *************/
    /// <summary>
    /// Checks if a user is in a specified role.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="role">The role to check against.</param>
    /// <returns>True if the user is in the specified role; otherwise, false.</returns>

/*******  01653b00-6111-4f0c-81dd-6fb3fd40d2e8  *******/
    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }
}
