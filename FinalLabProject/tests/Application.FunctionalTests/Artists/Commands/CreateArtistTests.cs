using FinalLabProject.Application.Artists.Commands.CreateArtist;
using FinalLabProject.Domain.Entities;
using FinalLabProject.Domain.Exceptions.Common;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FinalLabProject.Application.FunctionalTests.Artists.Commands;

using static Testing;

public class CreateArtistTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateArtistCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<Exception>();
    }

    [Test]
    public async Task ShouldCreateArtist()
    {
        var command = new CreateArtistCommand
        {
            Name = "Test Artist",
            Username = "testartista",
            Email = "testartista@example.com",
            Bio = "Test bio",
            PayoutTier = "Gold",
            Password = "ArtistTest123!"
        };

        var artistId = await SendAsync(command);

        var artist = await FindAsync<Artist>(artistId);

        artist.Should().NotBeNull();
        artist!.Name.Should().Be(command.Name);
        artist.Username.Value.Should().Be(command.Username);
        artist.Email.Value.Should().Be(command.Email);
        artist.Bio.Should().Be(command.Bio);
        artist.PayoutTier.Name.Should().Be(command.PayoutTier);
        artist.UserId.Should().NotBeNullOrEmpty();
    }

    [Test]
    public async Task ShouldNotAllowDuplicateEmail()
    {
        var command = new CreateArtistCommand
        {
            Name = "Artist1",
            Username = "artist1",
            Email = "duplicate@example.com",
            Bio = "Bio1",
            PayoutTier = "Bronze",
            Password = "ArtistTest123!"
        };

        await SendAsync(command);

        var duplicateCommand = new CreateArtistCommand
        {
            Name = "Artist2",
            Username = "artist2",
            Email = "duplicate@example.com",
            Bio = "Bio2",
            PayoutTier = "Silver",
            Password = "ArtistTest123!"
        };

        await FluentActions.Invoking(() =>
            SendAsync(duplicateCommand)).Should().ThrowAsync<Exception>();
    }
}