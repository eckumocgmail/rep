using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MarketerWeb.Authorization.Models;
using Newtonsoft.Json;

namespace MarketerWeb.Authorization;

public class UserService
{
    public UserService()
    {
        
    }

    public async Task<User?> FindUserFromDatabaseAsync(string username, string password)
    {
        var users = new List<User>()
        {
            new()
            {
                Username = "admin",
                Password = "password",
                UserId = "batov.ka",
                Roles = new()
                {
                    "MarketerEditor","MarketerCreator","MarketerReader"
                }
            },
            new()
            {
                Username = "MarketerReader",
                Password = "password",
                UserId = "batov.ka",
                Roles = new()
                {
                    "MarketerReader"
                }
            },
            new()
            {
                Username = "MarketerEditor",
                Password = "password",
                UserId = "batov.ka",
                Roles = new()
                {
                    "MarketerEditor","MarketerCreator","MarketerReader"
                }
            }
        };
        await Task.CompletedTask;
        var userInDatabase = users.FirstOrDefault(u => u.Username == username && u.Password == password);         
        return userInDatabase;
    }

}