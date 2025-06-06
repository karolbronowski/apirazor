using System.Data.Common;
using FinalLabProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinalLabProject.Application.FunctionalTests;

public class TestcontainersTestDatabase : ITestDatabase
{
    public async Task InitialiseAsync()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FinalLabProjectDb_Test")
            .Options;

        var context = new ApplicationDbContext(options);

        await context.Database.EnsureCreatedAsync();
    }

    public DbConnection GetConnection()
    {
        // InMemory provider does not use DbConnection
        throw new NotSupportedException("InMemory provider does not support DbConnection.");
    }

    public Task ResetAsync()
    {
        // For InMemory, you can recreate the context or clear data as needed
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        // Nothing to dispose for InMemory
        return Task.CompletedTask;
    }
}