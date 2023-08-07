using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniProject_UserManagement;
using MiniProject_UserManagement.Models;
using System.Net;

[TestClass]
public class GroupControllerIntegrationTests
{
    private readonly HttpClient _client;

    public GroupControllerIntegrationTests()
    {
        // Set up the test web application
        var appFactory = new WebApplicationFactory<Startup>();
        _client = appFactory.CreateClient();
    }

    [TestMethod]
    public async Task GetGroups_Should_ReturnListOfGroups()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("api/Group");
        response.EnsureSuccessStatusCode(); // Throws an exception if not successful

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var groups = await response.Content.ReadFromJsonAsync<Group[]>();
        Assert.IsNotNull(groups);
        Assert.IsTrue(groups.Length > 0);
    }

    [TestMethod]
    public async Task GetGroup_Should_ReturnGroupById()
    {
        // Arrange
        int groupId = 1; // Replace with a valid group ID

        // Act
        var response = await _client.GetAsync($"api/Group/{groupId}");
        response.EnsureSuccessStatusCode(); // Throws an exception if not successful

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var group = await response.Content.ReadFromJsonAsync<Group>();
        Assert.IsNotNull(group);
        Assert.AreEqual(groupId, group.Id);
        // Add more assertions for other properties as needed
    }

    [TestMethod]
    public async Task CreateGroup_Should_ReturnCreatedGroup()
    {
        // Arrange
        var newGroup = new Group
        {
            Name = "Test Group",
        };

        // Act
        var response = await _client.PostAsJsonAsync("api/Group", newGroup);
        response.EnsureSuccessStatusCode(); // Throws an exception if not successful

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var createdGroup = await response.Content.ReadFromJsonAsync<Group>();
        Assert.IsNotNull(createdGroup);
        Assert.AreEqual(newGroup.Name, createdGroup.Name);
        // Add more assertions for other properties as needed
    }

    [TestMethod]
    public async Task UpdateGroup_Should_ReturnNoContent()
    {
        // Arrange
        int groupId = 1; // Replace with a valid group ID
        var updatedGroup = new Group
        {
            Id = groupId,
            Name = "Updated Group",
        };

        // Act
        var response = await _client.PutAsJsonAsync($"api/Group/{groupId}", updatedGroup);
        response.EnsureSuccessStatusCode(); // Throws an exception if not successful

        // Assert
        Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

        // Check if the group has been updated in the database
        var getGroupResponse = await _client.GetAsync($"api/Group/{groupId}");
        Assert.AreEqual(HttpStatusCode.OK, getGroupResponse.StatusCode);
        var updatedGroupResponse = await getGroupResponse.Content.ReadFromJsonAsync<Group>();
        Assert.IsNotNull(updatedGroupResponse);
        Assert.AreEqual(updatedGroup.Name, updatedGroupResponse.Name);
        // Add more assertions for other properties as needed
    }

    [TestMethod]
    public async Task DeleteGroup_Should_ReturnNoContent()
    {
        // Arrange
        int groupId = 3; // Replace with a valid group ID

        // Act
        var response = await _client.DeleteAsync($"api/Group/{groupId}");
        response.EnsureSuccessStatusCode(); // Throws an exception if not successful

        // Assert
        Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

        // Check if the group has been deleted from the database
        var getGroupResponse = await _client.GetAsync($"api/Group/{groupId}");
        Assert.AreEqual(HttpStatusCode.NotFound, getGroupResponse.StatusCode);
    }
}