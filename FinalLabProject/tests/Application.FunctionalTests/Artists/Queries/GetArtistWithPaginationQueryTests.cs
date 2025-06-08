using FinalLabProject.Application.Artists.Commands.CreateArtist;
using FinalLabProject.Application.Artists.Queries.GetArtistsWithPagination;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FinalLabProject.Application.FunctionalTests.Artists.Queries;

using static Testing;

public class GetArtistsWithPaginationQueryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnPaginatedArtists()
    {
        // Create multiple artists
        for (int i = 1; i <= 15; i++)
        {
            await SendAsync(new CreateArtistCommand
            {
                Name = $"Artist {i}",
                Username = $"artistos{i}",
                Email = $"artistos{i}@example.com",
                Bio = $"Bio {i}",
                PayoutTier = "Gold",
                Password = "ArtistTest123!"
            });
        }

        var query = new GetArtistsWithPaginationQuery
        {
            PageNumber = 2,
            PageSize = 5
        };

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.Items.Should().HaveCount(5);
        result.TotalCount.Should().BeGreaterOrEqualTo(15);
        result.PageNumber.Should().Be(2);
    }

    [Test]
    public async Task ShouldReturnFilteredArtists()
    {
        await SendAsync(new CreateArtistCommand
        {
            Name = "SpecialArtist",
            Username = "specialartist",
            Email = "specialartist@example.com",
            Bio = "Special Bio",
            PayoutTier = "Gold",
            Password = "ArtistTest123!"
        });

        await SendAsync(new CreateArtistCommand
        {
            Name = "OtherArtist",
            Username = "otherartista",
            Email = "otherartista@example.com",
            Bio = "Other Bio",
            PayoutTier = "Gold",
            Password = "ArtistTest123!"
        });

        var query = new GetArtistsWithPaginationQuery
        {
            NameFilter = "SpecialArtist"
        };

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.Items.Should().ContainSingle(x => x.Name == "SpecialArtist");
    }
}