using FinalLabProject.Application.Artists.Commands.CreateArtist;
using FinalLabProject.Application.Songs.Commands.CreateSong;
using FinalLabProject.Domain.Entities;
using FinalLabProject.Domain.Exceptions.SongExceptions;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FinalLabProject.Application.FunctionalTests.Songs.Commands;

using static Testing;

public class CreateSongTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateSongCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<System.Exception>();
    }

    [Test]
    public async Task ShouldCreateSong()
    {
        var artistId = await SendAsync(new CreateArtistCommand
        {
            Name = "Song Artist",
            Username = "songartist",
            Email = "songartist@example.com",
            Bio = "Bio",
            PayoutTier = "Gold",
            Password = "ArtistTest123!"
        });

        var command = new CreateSongCommand
        {
            Title = "Test Song",
            ArtistId = artistId
        };

        var songId = await SendAsync(command);

        var song = await FindAsync<Song>(songId);

        song.Should().NotBeNull();
        song!.Title.Should().Be(command.Title);
        song.ArtistId.Should().Be(artistId);
        song.ListenedTimes.Should().Be(0);
    }

    [Test]
    public async Task ShouldNotAllowDuplicateSongTitleForArtist()
    {
        var artistId = await SendAsync(new CreateArtistCommand
        {
            Name = "Dup Song Artist",
            Username = "dupsongartist",
            Email = "dupsongartist@example.com",
            Bio = "Bio",
            PayoutTier = "Gold",
            Password = "ArtistTest123!"
        });

        var command = new CreateSongCommand
        {
            Title = "Duplicate Song",
            ArtistId = artistId
        };

        await SendAsync(command);

        var duplicateCommand = new CreateSongCommand
        {
            Title = "Duplicate Song",
            ArtistId = artistId
        };

        await FluentActions.Invoking(() =>
            SendAsync(duplicateCommand)).Should().ThrowAsync<SongAlreadyExistsException>();
    }
}