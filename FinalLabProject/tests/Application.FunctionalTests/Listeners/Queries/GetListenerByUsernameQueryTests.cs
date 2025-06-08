using FinalLabProject.Application.Listeners.Commands.CreateListener;
using FinalLabProject.Application.Listeners.Queries.GetListenerByUsername;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FinalLabProject.Application.FunctionalTests.Listeners.Queries;

using static Testing;

public class GetListenerByUsernameQueryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnListenerByUsername()
    {
        var listenerId = await SendAsync(new CreateListenerCommand
        {
            Name = "Listener By Username",
            Username = "uniqueusername",
            Email = "uniqueusername@example.com",
            Password = "ListenerTest123!"
        });

        var query = new GetListenerByUsernameQuery("uniqueusername");

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result!.Id.Should().Be(listenerId);
        result.Name.Should().Be("Listener By Username");
        result.Username.Should().Be("uniqueusername");
        result.Email.Should().Be("uniqueusername@example.com");
    }

    [Test]
    public async Task ShouldReturnNullForNonExistingUsername()
    {
        var query = new GetListenerByUsernameQuery("nonexistingusername");

        var result = await SendAsync(query);

        result.Should().BeNull();
    }
}