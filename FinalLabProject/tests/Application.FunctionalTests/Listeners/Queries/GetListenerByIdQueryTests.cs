using FinalLabProject.Application.Listeners.Commands.CreateListener;
using FinalLabProject.Application.Listeners.Queries.GetListenerById;
using FinalLabProject.Domain.Entities;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FinalLabProject.Application.FunctionalTests.Listeners.Queries;

using static Testing;

public class GetListenerByIdQueryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnListenerById()
    {
        var listenerId = await SendAsync(new CreateListenerCommand
        {
            Name = "Test Listener",
            Username = "testlistenerbyid",
            Email = "testlistenerbyid@example.com",
            Password = "ListenerTest123!"
        });

        var query = new GetListenerByIdQuery(listenerId);

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result!.Id.Should().Be(listenerId);
        result.Name.Should().Be("Test Listener");
        result.Username.Should().Be("testlistenerbyid");
        result.Email.Should().Be("testlistenerbyid@example.com");
    }

    [Test]
    public async Task ShouldReturnNullForNonExistingListener()
    {
        var query = new GetListenerByIdQuery(9999);

        var result = await SendAsync(query);

        result.Should().BeNull();
    }
}