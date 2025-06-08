using FinalLabProject.Application.Listeners.Commands.CreateListener;
using FinalLabProject.Application.Listeners.Commands.UpdateListener;
using FinalLabProject.Domain.Entities;
using FinalLabProject.Domain.Exceptions.ListenerExceptions;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FinalLabProject.Application.FunctionalTests.Listeners.Commands;

using static Testing;

public class UpdateListenerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldThrowIfListenerNotFound()
    {
        var command = new UpdateListenerCommand
        {
            Id = 999,
            Name = "Updated Name"
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ListenerNotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateListenerName()
    {
        var listenerId = await SendAsync(new CreateListenerCommand
        {
            Name = "Original Name",
            Username = "updatelistener",
            Email = "updatelistener@example.com",
            Password = "ListenerTest123!"
        });

        var updateCommand = new UpdateListenerCommand
        {
            Id = listenerId,
            Name = "Updated Name"
        };

        await SendAsync(updateCommand);

        var listener = await FindAsync<Listener>(listenerId);

        listener.Should().NotBeNull();
        listener!.Name.Should().Be("Updated Name");
    }
}