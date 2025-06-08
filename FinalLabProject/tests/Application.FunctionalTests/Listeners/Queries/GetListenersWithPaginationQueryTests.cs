using FinalLabProject.Application.Listeners.Commands.CreateListener;
using FinalLabProject.Application.Listeners.Queries.GetListenersWithPagination;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FinalLabProject.Application.FunctionalTests.Listeners.Queries;

using static Testing;

public class GetListenersWithPaginationQueryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnPaginatedListeners()
    {
        // Create multiple listeners
        for (int i = 1; i <= 15; i++)
        {
            await SendAsync(new CreateListenerCommand
            {
                Name = $"Listener {i}",
                Username = $"listener{i}",
                Email = $"listener{i}@example.com",
                Password = "ListenerTest123!"
            });
        }

        var query = new GetListenersWithPaginationQuery
        {
            PageNumber = 2,
            PageSize = 5
        };

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.Items.Should().HaveCount(5);
        result.TotalCount.Should().BeGreaterOrEqualTo(15);
        result.PageNumber.Should().Be(2);
    }

    [Test]
    public async Task ShouldReturnFilteredListeners()
    {
        await SendAsync(new CreateListenerCommand
        {
            Name = "SpecialName",
            Username = "specialusername",
            Email = "special@example.com",
            Password = "ListenerTest123!"
        });

        await SendAsync(new CreateListenerCommand
        {
            Name = "OtherName",
            Username = "otherusername",
            Email = "other@example.com",
            Password = "ListenerTest123!"
        });

        var query = new GetListenersWithPaginationQuery
        {
            NameFilter = "SpecialName"
        };

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.Items.Should().ContainSingle(x => x.Name == "SpecialName");
    }
}