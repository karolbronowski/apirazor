using FinalLabProject.Application.Artists.Commands.CreateArtist;
using FinalLabProject.Application.Songs.Commands.CreateSong;
using FinalLabProject.Application.Songs.Queries.GetSongsWithPagination;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FinalLabProject.Application.FunctionalTests.Songs.Queries;

using static Testing;

public class GetSongsWithPaginationQueryTests : BaseTestFixture
{
    [SetUp]
    public async Task SetUp()
    {
        await Testing.ResetState();
    }
    [Test]
    public async Task ShouldReturnPaginatedSongs()
    {
        var artistId = await SendAsync(new CreateArtistCommand
        {
            Name = "Pagination Artist",
            Username = "paginationartistforsongs",
            Email = "paginationartistforsongs@example.com",
            Bio = "Bio",
            PayoutTier = "Gold",
            Password = "ArtistTest123!"
        });

        // Create 12 songs for this artist
        for (int i = 1; i <= 12; i++)
        {
            await SendAsync(new CreateSongCommand
            {
                Title = $"Song {i}",
                ArtistId = artistId
            });
        }

        var query = new GetSongsWithPaginationQuery
        {
            PageNumber = 2,
            PageSize = 5
        };

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.Items.Should().HaveCount(5);
        result.TotalCount.Should().BeGreaterOrEqualTo(12);
        result.PageNumber.Should().Be(2);
    }

    [Test]
    public async Task ShouldReturnEmptyIfNoSongs()
    {
        var query = new GetSongsWithPaginationQuery
        {
            PageNumber = 1,
            PageSize = 10
        };

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.Items.Should().BeEmpty();
        result.TotalCount.Should().Be(0);
    }
}