using System.Data.Common;
using FinalLabProject.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Respawn;

namespace FinalLabProject.Application.FunctionalTests;

public class SqlServerTestDatabase : ITestDatabase
{
    private readonly string _connectionString = null!;
    private SqlConnection _connection = null!;
    private Respawner _respawner = null!;

    public SqlServerTestDatabase()
    {
        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(connectionString);

        _connectionString = connectionString;
    }

    public async Task InitialiseAsync()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("FinalLabProjectDb")
            .Options;   

        var context = new ApplicationDbContext(options);    

        // No migration needed for InMemory, but you can ensure database is created
        await context.Database.EnsureCreatedAsync();
    }   

    public DbConnection GetConnection()
    {
        // InMemory provider does not use DbConnection, so you can return null or throw
        throw new NotSupportedException("InMemory provider does not support DbConnection.");
    }   

    public async Task ResetAsync()
    {
        // For InMemory, you can just recreate the context or clear data as needed
    }   

    public async Task DisposeAsync()
    {
        // Nothing to dispose for InMemory
    }
}
