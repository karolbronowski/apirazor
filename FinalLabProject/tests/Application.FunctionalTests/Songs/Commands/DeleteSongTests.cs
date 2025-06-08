using FinalLabProject.Application.Artists.Commands.CreateArtist;
using FinalLabProject.Application.Songs.Commands.CreateSong;
using FinalLabProject.Application.Songs.Commands.DeleteSong;
using FinalLabProject.Domain.Entities;
using FinalLabProject.Domain.Exceptions.SongExceptions;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FinalLabProject.Application.FunctionalTests.Songs.Commands;

using static Testing;

public class DeleteSongTests : BaseTestFixture
{
    [Test]
    public async Task ShouldThrowIfSongNotFound()
    {
        var command = new DeleteSongCommand(9999);

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<SongNotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteSong()
    {
        var artistId = await SendAsync(new CreateArtistCommand
        {
            Name = "Delete Song Artist",
            Username = "deletesongartist",
            Email = "deletesongartist@example.com",
            Bio = "Bio",
            PayoutTier = "Gold",
            Password = "ArtistTest123!"
        });

        var songId = await SendAsync(new CreateSongCommand
        {
            Title = "Song To Delete",
            ArtistId = artistId
        });

        await SendAsync(new DeleteSongCommand(songId));

        var song = await FindAsync<Song>(songId);

        song.Should().BeNull();
    }
}