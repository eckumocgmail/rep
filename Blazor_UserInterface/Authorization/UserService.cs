using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Console_AuthModel.AuthorizationModel.UserModel;

namespace MarketerWeb.Authorization;

public class UserService
{
    public UserService()
    {
        
    }

    public async Task<UserContext?> FindUserFromDatabaseAsync(string username, string password)
    {
        var users = new List<UserContext>()
        {
             
            new()
            {
                Account = new UserAccount("eckumoc@gmail.com","eckumoc@gmail.com"),
                Roles = new List<UserRole>(){ 
                    new UserRole()
                    {
                        Code = "admin"
                    }
                }
            }
        };
        await Task.CompletedTask;
        var userInDatabase = users.FirstOrDefault(u => u.Account.Email.ToLower() == username.ToLower() && u.Account.Password == password);         
        return userInDatabase;
    }

}