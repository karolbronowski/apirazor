using FinalLabProject.Application.Artists.Commands.CreateArtist;
using FinalLabProject.Application.Songs.Commands.CreateSong;
using FinalLabProject.Application.Songs.Queries.GetArtistSongsWithPagination;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FinalLabProject.Application.FunctionalTests.Songs.Queries;

using static Testing;

public class GetArtistSongsWithPaginationQueryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnPaginatedSongsForArtist()
    {
        var artistId = await SendAsync(new CreateArtistCommand
        {
            Name = "Pagination Artist",
            Username = "paginationartist",
            Email = "paginationartist@example.com",
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

        var query = new GetArtistSongsWithPaginationQuery
        {
            ArtistId = artistId,
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
    public async Task ShouldReturnEmptyForArtistWithNoSongs()
    {
        var artistId = await SendAsync(new CreateArtistCommand
        {
            Name = "No Songs Artist",
            Username = "nosongsartist",
            Email = "nosongsartist@example.com",
            Bio = "Bio",
            PayoutTier = "Gold",
            Password = "ArtistTest123!"
        });

        var query = new GetArtistSongsWithPaginationQuery
        {
            ArtistId = artistId,
            PageNumber = 1,
            PageSize = 10
        };

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.Items.Should().BeEmpty();
        result.TotalCount.Should().Be(0);
    }
}