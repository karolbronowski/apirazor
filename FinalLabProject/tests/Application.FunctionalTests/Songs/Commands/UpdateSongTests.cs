using FinalLabProject.Application.Artists.Commands.CreateArtist;
using FinalLabProject.Application.Songs.Commands.CreateSong;
using FinalLabProject.Application.Songs.Commands.UpdateSong;
using FinalLabProject.Domain.Entities;
using Ardalis.GuardClauses;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FinalLabProject.Application.FunctionalTests.Songs.Commands;

using static Testing;

public class UpdateSongTests : BaseTestFixture
{
    [Test]
    public async Task ShouldThrowIfSongNotFound()
    {
        var command = new UpdateSongCommand
        {
            Id = 9999,
            Title = "Updated Title"
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateSongFields()
    {
        var artistId = await SendAsync(new CreateArtistCommand
        {
            Name = "Update Song Artist",
            Username = "updatesongartist",
            Email = "updatesongartist@example.com",
            Bio = "Bio",
            PayoutTier = "Gold",
            Password = "ArtistTest123!"
        });

        var songId = await SendAsync(new CreateSongCommand
        {
            Title = "Original Song",
            ArtistId = artistId
        });

        var updateCommand = new UpdateSongCommand
        {
            Id = songId,
            Title = "Updated Song Title"
        };

        await SendAsync(updateCommand);

        var song = await FindAsync<Song>(songId);

        song.Should().NotBeNull();
        song!.Title.Should().Be("Updated Song Title");
        song.ArtistId.Should().Be(artistId);
    }

    [Test]
    public async Task ShouldUpdateSongArtist()
    {
        var artistId1 = await SendAsync(new CreateArtistCommand
        {
            Name = "Artist 1",
            Username = "artist1forsongupdate",
            Email = "artist1forsongupdate@example.com",
            Bio = "Bio1",
            PayoutTier = "Gold",
            Password = "ArtistTest123!"
        });

        var artistId2 = await SendAsync(new CreateArtistCommand
        {
            Name = "Artist 2",
            Username = "artist2forsongupdate",
            Email = "artist2forsongupdate@example.com",
            Bio = "Bio2",
            PayoutTier = "Gold",
            Password = "ArtistTest123!"
        });

        var songId = await SendAsync(new CreateSongCommand
        {
            Title = "Song To Change Artist",
            ArtistId = artistId1
        });

        var updateCommand = new UpdateSongCommand
        {
            Id = songId,
            ArtistId = artistId2
        };

        await SendAsync(updateCommand);

        var song = await FindAsync<Song>(songId);

        song.Should().NotBeNull();
        song!.ArtistId.Should().Be(artistId2);
    }
}