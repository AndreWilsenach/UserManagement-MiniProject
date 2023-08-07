using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniProject_UserManagement.Models;
using MiniProject_UserManagement;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

[TestClass]
public class UserControllerIntegrationTests
{
    private readonly HttpClient _client;

    public UserControllerIntegrationTests()
    {
        // Set up the test web application
        var appFactory = new WebApplicationFactory<Startup>();
        _client = appFactory.CreateClient();
    }

    [TestMethod]
    public async Task GetUsers_Should_ReturnListOfUsers()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("api/User");
        response.EnsureSuccessStatusCode(); // Throws an exception if not successful

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var users = await response.Content.ReadFromJsonAsync<User[]>();
        Assert.IsNotNull(users);
        Assert.IsTrue(users.Length > 0);
    }

    [TestMethod]
    public async Task GetUser_Should_ReturnUserById()
    {
        // Arrange
        int userId = 1; // Replace with a valid user ID

        // Act
        var response = await _client.GetAsync($"api/User/{userId}");
        response.EnsureSuccessStatusCode(); // Throws an exception if not successful

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var user = await response.Content.ReadFromJsonAsync<User>();
        Assert.IsNotNull(user);
        Assert.AreEqual(userId, user.Id);
        // Add more assertions for other properties as needed
    }

    [TestMethod]
    public async Task CreateUser_Should_ReturnCreatedUser()
    {
        // Arrange
        var newUser = new User
        {
            Name = "John",
            Surname = "Doe",
            IdNumber = "1234567890",
            ContactDetail = "john@example.com"
        };

        // Act
        var response = await _client.PostAsJsonAsync("api/User", newUser);
        response.EnsureSuccessStatusCode(); // Throws an exception if not successful

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var createdUser = await response.Content.ReadFromJsonAsync<User>();
        Assert.IsNotNull(createdUser);
        Assert.AreEqual(newUser.Name, createdUser.Name);
        // Add more assertions for other properties as needed
    }

    [TestMethod]
    public async Task UpdateUser_Should_ReturnUpdatedUser()
    {
        // Arrange
        int userId = 1; // Replace with a valid user ID
        var updatedUser = new User
        {
            Id = userId,
            Name = "UpdatedName",
            Surname = "UpdatedSurname",
            IdNumber = "UpdatedIDNumber",
            ContactDetail = "updated@example.com"
        };

        // Act
        var response = await _client.PutAsJsonAsync($"api/User/{userId}", updatedUser);
        response.EnsureSuccessStatusCode(); // Throws an exception if not successful

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var updatedUserResponse = await response.Content.ReadFromJsonAsync<User>();
        Assert.IsNotNull(updatedUserResponse);
        Assert.AreEqual(updatedUser.Name, updatedUserResponse.Name);
        // Add more assertions for other properties as needed
    }

    [TestMethod]
    public async Task DeleteUser_Should_ReturnNoContent()
    {
        // Arrange
        int userId = 2; // Replace with a valid user ID

        // Act
        var response = await _client.DeleteAsync($"api/User/{userId}");
        response.EnsureSuccessStatusCode(); // Throws an exception if not successful

        // Assert
        Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

        // Check if the user has been deleted from the database
        var getUserResponse = await _client.GetAsync($"api/User/{userId}");
        Assert.AreEqual(HttpStatusCode.NotFound, getUserResponse.StatusCode);
    }

    [TestMethod]
    public async Task GetUserCount_Should_ReturnNumberOfUsers()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("api/User/userCount");
        response.EnsureSuccessStatusCode(); // Throws an exception if not successful

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        int userCount = await response.Content.ReadFromJsonAsync<int>();
        Assert.IsTrue(userCount >= 0);
    }
}