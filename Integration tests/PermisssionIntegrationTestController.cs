using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniProject_UserManagement.Models;
using MiniProject_UserManagement;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

[TestClass]
public class PermissionControllerIntegrationTests
{
    private readonly HttpClient _client;

    public PermissionControllerIntegrationTests()
    {
        // Set up the test web application
        var appFactory = new WebApplicationFactory<Startup>();
        _client = appFactory.CreateClient();
    }

    [TestMethod]
    public async Task GetPermissions_Should_ReturnListOfPermissions()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("api/Permission");
        response.EnsureSuccessStatusCode(); // Throws an exception if not successful

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var permissions = await response.Content.ReadFromJsonAsync<Permission[]>();
        Assert.IsNotNull(permissions);
        Assert.IsTrue(permissions.Length > 0);
    }

    [TestMethod]
    public async Task GetPermission_Should_ReturnPermissionById()
    {
        // Arrange
        int permissionId = 1; // Replace with a valid permission ID

        // Act
        var response = await _client.GetAsync($"api/Permission/{permissionId}");
        response.EnsureSuccessStatusCode(); // Throws an exception if not successful

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var permission = await response.Content.ReadFromJsonAsync<Permission>();
        Assert.IsNotNull(permission);
        Assert.AreEqual(permissionId, permission.Id);
        // Add more assertions for other properties as needed
    }

    [TestMethod]
    public async Task CreatePermission_Should_ReturnCreatedPermission()
    {
        // Arrange
        var newPermission = new Permission
        {
            Name = "Test Permission",
            Description = "Test Description"
        };

        // Act
        var response = await _client.PostAsJsonAsync("api/Permission", newPermission);
        response.EnsureSuccessStatusCode(); // Throws an exception if not successful

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var createdPermission = await response.Content.ReadFromJsonAsync<Permission>();
        Assert.IsNotNull(createdPermission);
        Assert.AreEqual(newPermission.Name, createdPermission.Name);
        Assert.AreEqual(newPermission.Description, createdPermission.Description);
        // Add more assertions for other properties as needed
    }

    [TestMethod]
    public async Task UpdatePermission_Should_ReturnNoContent()
    {
        // Arrange
        int permissionId = 1; // Replace with a valid permission ID
        var updatedPermission = new Permission
        {
            Id = permissionId,
            Name = "Updated Permission",
            Description = "Updated Description"
        };

        // Act
        var response = await _client.PutAsJsonAsync($"api/Permission/{permissionId}", updatedPermission);
        response.EnsureSuccessStatusCode(); // Throws an exception if not successful

        // Assert
        Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

        // Check if the permission has been updated in the database
        var getPermissionResponse = await _client.GetAsync($"api/Permission/{permissionId}");
        Assert.AreEqual(HttpStatusCode.OK, getPermissionResponse.StatusCode);
        var updatedPermissionResponse = await getPermissionResponse.Content.ReadFromJsonAsync<Permission>();
        Assert.IsNotNull(updatedPermissionResponse);
        Assert.AreEqual(updatedPermission.Name, updatedPermissionResponse.Name);
        Assert.AreEqual(updatedPermission.Description, updatedPermissionResponse.Description);
        // Add more assertions for other properties as needed
    }

    [TestMethod]
    public async Task DeletePermission_Should_ReturnNoContent()
    {
        // Arrange
        int permissionId = 3; // Replace with a valid permission ID

        // Act
        var response = await _client.DeleteAsync($"api/Permission/{permissionId}");
        response.EnsureSuccessStatusCode(); // Throws an exception if not successful

        // Assert
        Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

        // Check if the permission has been deleted from the database
        var getPermissionResponse = await _client.GetAsync($"api/Permission/{permissionId}");
        Assert.AreEqual(HttpStatusCode.NotFound, getPermissionResponse.StatusCode);
    }
}