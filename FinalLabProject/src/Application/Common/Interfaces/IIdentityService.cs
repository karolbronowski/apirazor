using FinalLabProject.Application.Common.Models;
using FinalLabProject.Domain.Enums;

namespace FinalLabProject.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string email, string password, UserType userType);

    Task<Result> DeleteUserAsync(string userId);
}
