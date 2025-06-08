using FinalLabProject.Application.Artists.Commands.CreateArtist;
using FinalLabProject.Application.Songs.Commands.CreateSong;
using FinalLabProject.Application.Songs.Commands.PlaySong;
using FinalLabProject.Domain.Entities;
using Ardalis.GuardClauses;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FinalLabProject.Application.FunctionalTests.Songs.Commands;

using static Testing;

public class PlaySongTests : BaseTestFixture
{
    [Test]
    public async Task ShouldThrowIfSongNotFound()
    {
        var command = new PlaySongCommand(9999);

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldIncrementListenedTimes()
    {
        var artistId = await SendAsync(new CreateArtistCommand
        {
            Name = "Play Song Artist",
            Username = "playsongartist",
            Email = "playsongartist@example.com",
            Bio = "Bio",
            PayoutTier = "Gold",
            Password = "ArtistTest123!"
        });

        var songId = await SendAsync(new CreateSongCommand
        {
            Title = "Song To Play",
            ArtistId = artistId
        });

        var song = await FindAsync<Song>(songId);
        song.Should().NotBeNull();
        song!.ListenedTimes.Should().Be(0);

        await SendAsync(new PlaySongCommand(songId));

        song = await FindAsync<Song>(songId);
        song!.ListenedTimes.Should().Be(1);

        // Play again
        await SendAsync(new PlaySongCommand(songId));
        song = await FindAsync<Song>(songId);
        song!.ListenedTimes.Should().Be(2);
    }
}