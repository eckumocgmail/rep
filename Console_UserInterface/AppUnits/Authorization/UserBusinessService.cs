using Console_DataConnector.DataModule.DataModels.MessageModel;

using Console_UserInterface.ControlAttributes;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// 
/// </summary>
public class UserBusinessService
{
    private readonly DbContextUser dbu;

    public UserBusinessService(DbContextUser dbu)
    {
        this.dbu = dbu;
    }

    public List<UserRole> GetUserRoles(int userId)
    {
        var userRoleIds = dbu.UsersRoles_.Where(sr => sr.UserId == userId).Select(sr => sr.RoleId).ToList();
        return this.dbu.UserRoles_.Where(r => userRoleIds.Contains(r.Id)).ToList();
    }

    public List<MessageProtocol> GetMessageProtocols([Required][Label("Пользователь")][SelectData($"{nameof(UserPerson)},{nameof(UserPerson.LastName)}")]int userId)
    {
        using (var mes = new MessageDbContext())
        {
            return mes.MessageProtocols.Include(p => p.Properties).Where(p => dbu.RoleFunctions.Where(rf => GetUserRoles(userId).Select(r => r.Id).Contains(rf.UserRoleId)).Select(rf => rf.MessageInput).Contains(p.Id)).ToList();
        } 
    }
}