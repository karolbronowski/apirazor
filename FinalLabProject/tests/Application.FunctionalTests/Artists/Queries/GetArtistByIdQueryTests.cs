using FinalLabProject.Application.Artists.Commands.CreateArtist;
using FinalLabProject.Application.Artists.Queries.GetArtistById;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FinalLabProject.Application.FunctionalTests.Artists.Queries;

using static Testing;

public class GetArtistByIdQueryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnArtistById()
    {
        var artistId = await SendAsync(new CreateArtistCommand
        {
            Name = "Test Artist",
            Username = "testartistbyid",
            Email = "testartistbyid@example.com",
            Bio = "Test bio",
            PayoutTier = "Gold",
            Password = "ArtistTest123!"
        });

        var query = new GetArtistByIdQuery(artistId);

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result!.Id.Should().Be(artistId);
        result.Name.Should().Be("Test Artist");
        result.Username.Should().Be("testartistbyid");
        result.Email.Should().Be("testartistbyid@example.com");
        result.Bio.Should().Be("Test bio");
        result.PayoutTier.Should().Be("Gold");
    }

    [Test]
    public async Task ShouldReturnNullForNonExistingArtist()
    {
        var query = new GetArtistByIdQuery(9999);

        var result = await SendAsync(query);

        result.Should().BeNull();
    }
}