using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework.Internal;

[Ignore("Need to work on app settings structure and start up local db")]
public class TaskManagerControllerTests
{
    private WebApplicationFactory<Program> factory;

    [SetUp]
    public void SetUp()
    {
        factory = new WebApplicationFactory<Program>();
    }

    [TearDown]
    public void TearDown()
    {
        factory.Dispose();
    }

    [Test]
    public async Task GetAll()
    {
        var client = factory.CreateClient();
        var resp = await client.GetAsync("api/TaskManager");
        resp.Should().NotBeNull();
    }
}