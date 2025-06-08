using FinalLabProject.Application.Listeners.Commands.CreateListener;
using FinalLabProject.Domain.Entities;
using FinalLabProject.Application.Common.Exceptions;
using FluentAssertions;
using System;
using System.Threading.Tasks;

namespace FinalLabProject.Application.FunctionalTests.Listeners.Commands;

using static Testing;

public class CreateListenerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateListenerCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateListener()
    {
        var command = new CreateListenerCommand
        {
            Name = "Functional Test Listener",
            Username = "functionaltestlistener",
            Email = "functionaltestlistener@example.com",
            Password = "ListenerTest123!"
        };

        var listenerId = await SendAsync(command);

        var listener = await FindAsync<Listener>(listenerId);

        listener.Should().NotBeNull();
        listener!.Name.Should().Be(command.Name);
        listener.Username.Value.Should().Be(command.Username);
        listener.Email.Value.Should().Be(command.Email);
        listener.UserId.Should().NotBeNullOrEmpty();
    }
}