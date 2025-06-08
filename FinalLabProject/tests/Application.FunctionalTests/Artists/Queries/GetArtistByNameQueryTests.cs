using FinalLabProject.Application.Artists.Commands.CreateArtist;
using FinalLabProject.Application.Artists.Queries.GetArtistByName;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Linq;

namespace FinalLabProject.Application.FunctionalTests.Artists.Queries;

using static Testing;

public class GetArtistByNameQueryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnArtistsByName()
    {
        var artistId1 = await SendAsync(new CreateArtistCommand
        {
            Name = "Matching Artist",
            Username = "matchingartist1",
            Email = "matchingartist1@example.com",
            Bio = "Bio1",
            PayoutTier = "Gold",
            Password = "ArtistTest123!"
        });

        var artistId2 = await SendAsync(new CreateArtistCommand
        {
            Name = "Matching Artist",
            Username = "matchingartist2",
            Email = "matchingartist2@example.com",
            Bio = "Bio2",
            PayoutTier = "Gold",
            Password = "ArtistTest123!"
        });

        var artistId3 = await SendAsync(new CreateArtistCommand
        {
            Name = "Other Artist",
            Username = "otherartist",
            Email = "otherartist@example.com",
            Bio = "Bio3",
            PayoutTier = "Gold",
            Password = "ArtistTest123!"
        });

        var query = new GetArtistByNameQuery("Matching Artist");

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Any(a => a.Id == artistId1).Should().BeTrue();
        result.Any(a => a.Id == artistId2).Should().BeTrue();
    }

    [Test]
    public async Task ShouldReturnEmptyListForNonExistingName()
    {
        var query = new GetArtistByNameQuery("NonExistingArtistName");

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
}