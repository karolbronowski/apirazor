using System.Runtime.InteropServices;
using FinalLabProject.Domain.Constants;
using FinalLabProject.Domain.Entities;
using FinalLabProject.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FinalLabProject.Domain.ValueObjects;

namespace FinalLabProject.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.EnsureCreatedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Ensure roles exist
        var requiredRoles = new[] { Roles.Administrator, Roles.Artist, Roles.Listener };
        foreach (var roleName in requiredRoles)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Default admin user
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            await _userManager.AddToRoleAsync(administrator, Roles.Administrator);
        }

        // Seed TodoList
        if (!_context.TodoLists.Any())
        {
            _context.TodoLists.Add(new TodoList
            {
                Title = "Todo List",
                Items =
                {
                    new TodoItem { Title = "Make a todo list 📃" },
                    new TodoItem { Title = "Check off the first item ✅" },
                    new TodoItem { Title = "Realise you've already done two things on the list! 🤯"},
                    new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" },
                }
            });

            await _context.SaveChangesAsync();
        }

        // Seed Artist user + Artist entity
        if (!_context.Artists.Any())
        {
            var artistUser = new ApplicationUser
            {
                UserName = "testartist",
                Email = "artist@example.com"
            };
            var artistUserResult = await _userManager.CreateAsync(artistUser, "Artist1!");
            if (artistUserResult.Succeeded)
            {
                // Assign to Artist role
                await _userManager.AddToRoleAsync(artistUser, Roles.Artist);

                var artist = new Artist
                {
                    UserId = artistUser.Id,
                    Name = "Test Artist",
                    Username = new Username("testartist"),
                    Email = new EmailAddress("artist@example.com"),
                    Bio = "A test artist.",
                    PayoutTier = new PayoutTier("Bronze")
                };
                _context.Artists.Add(artist);
                await _context.SaveChangesAsync();
            }
        }

        // Seed Song
        if (!_context.Songs.Any())
        {
            var artist = _context.Artists.First();
            var songs = new List<Song>
            {
                new Song
                {
                    Title = "Test Song",
                    ArtistId = artist.Id,
                    ListenedTimes = 0,
                    Created = DateTimeOffset.UtcNow,
                    LastModified = DateTimeOffset.UtcNow
                },
                new Song
                {
                    Title = "Second Song",
                    ArtistId = artist.Id,
                    ListenedTimes = 5,
                    Created = DateTimeOffset.UtcNow,
                    LastModified = DateTimeOffset.UtcNow
                },
                new Song
                {
                    Title = "Third Song",
                    ArtistId = artist.Id,
                    ListenedTimes = 2,
                    Created = DateTimeOffset.UtcNow,
                    LastModified = DateTimeOffset.UtcNow
                },
                new Song
                {
                    Title = "Fourth Song",
                    ArtistId = artist.Id,
                    ListenedTimes = 10,
                    Created = DateTimeOffset.UtcNow,
                    LastModified = DateTimeOffset.UtcNow
                }
            };
            _context.Songs.AddRange(songs);
            await _context.SaveChangesAsync();
        }


        // Seed Listener user + Listener entity
        if (!_context.Listeners.Any())
        {
            var listenerUser = new ApplicationUser
            {
                UserName = "testlistener",
                Email = "listener@example.com"
            };
            var listenerUserResult = await _userManager.CreateAsync(listenerUser, "Listener1!");
            if (listenerUserResult.Succeeded)
            {
                // Assign to Listener role
                await _userManager.AddToRoleAsync(listenerUser, Roles.Listener);

                var listener = new Listener
                {
                    UserId = listenerUser.Id,
                    Name = "Test Listener",
                    Username = new Username("testlistener"),
                    Email = new EmailAddress("listener@example.com"),
                };
                _context.Listeners.Add(listener);
                await _context.SaveChangesAsync();      

                // Assign favourite songs after both listener and songs are seeded
                var firstSong = _context.Songs.FirstOrDefault();
                if (firstSong != null)
                {
                    listener.FavouriteSongs.Add(firstSong);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}