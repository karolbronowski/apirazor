using FinalLabProject.Application.Artists.Commands.CreateArtist;
using FinalLabProject.Application.Artists.Commands.UpdateArtist;
using FinalLabProject.Domain.Entities;
using Ardalis.GuardClauses;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FinalLabProject.Application.FunctionalTests.Artists.Commands;

using static Testing;

public class UpdateArtistTests : BaseTestFixture
{
    [Test]
    public async Task ShouldThrowIfArtistNotFound()
    {
        var command = new UpdateArtistCommand
        {
            Id = 9999,
            Name = "Updated Name",
            Bio = "Updated Bio"
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateArtistFields()
    {
        var artistId = await SendAsync(new CreateArtistCommand
        {
            Name = "Original Name",
            Username = "updateartist",
            Email = "updateartist@example.com",
            Bio = "Original Bio",
            PayoutTier = "Gold",
            Password = "ArtistTest123!"
        });

        var updateCommand = new UpdateArtistCommand
        {
            Id = artistId,
            Name = "Updated Name",
            Bio = "Updated Bio"
        };

        await SendAsync(updateCommand);

        var artist = await FindAsync<Artist>(artistId);

        artist.Should().NotBeNull();
        artist!.Name.Should().Be("Updated Name");
        artist.Bio.Should().Be("Updated Bio");
    }
}