
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


/// <summary>
/// Служба уровня приложения, содержит пользователей сеансов
/// </summary>
public class AuthorizationUsers: AuthorizationCollection<UserContext>, APIUsers
{
    public AuthorizationUsers(  AuthorizationOptions options ): base( options){
    }
    /// <summary>
    /// Проверка наличия обьекта с заданным ключом
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetByEmail(string email)
    {
        var p = this._memory.Where(u => u.Value is not null && u.Value.Account is not null && u.Value.Account.Email is not null &&
            u.Value.Account.Email.ToUpper() == email.ToUpper());
        if (p is null || p.Count() == 0)
            return null;        
        return p.First().Key;
    }
    public UserContext FindByEmail(string email)
    {
        var p = this._memory.Where(u => u.Value.Account.Email.ToUpper() == email.ToUpper());
        if (p is null || p.Count() == 0)
            return null;
        return p.First().Value;
    }

    private IEnumerable<string> GetUserGroups(UserContext user)
    {
        using (var db = new DbContextUser())
        {
            return db.UserGroups_.Where(g => user.UserGroups.Select(group => group.GroupID).Contains(g.Id)).Select(g => g.Name);
        }            
    }
    private IEnumerable<string> GetUserRoles(UserContext user)
    {
        using (var db = new DbContextUser())
        {
            return db.UserRoles_.Where(g => user.BusinessFunctions.Select(group => group.RoleId).Contains(g.Id)).Select(g => g.Code);
        }
    }
    public IEnumerable<UserContext> FindByGroup(string group)
    {
        using (var db = new DbContextUser())
        {
            return GetAll().Where(user => GetUserGroups(user).Contains(group));            
        }            
    }

    public IEnumerable<UserContext> FindByRole(string role)
    {
        using (var db = new DbContextUser())
        {
            return GetAll().Where(user => GetUserRoles(user).Contains(role));
        }
    }

    public UserContext FindByToken(string token)
    {
        if (String.IsNullOrWhiteSpace(token))
            throw new ArgumentException(nameof(token));
        if (this._memory.ContainsKey(token))
        {            
            return this._memory[token];
        }
        return null;
    }

    
}

