
using Console_AuthModel.AuthorizationModel.UserModel;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 

public interface IGroupService
{
    public IEnumerable<string> GetMyGroups();
    public IEnumerable<UserGroup> GetGroups(int page, int size);
    public IEnumerable<UserGroup> SearchGroups(string query, int page, int size);
    public IEnumerable<UserPerson> GetPeople(int groupId);
    public IEnumerable<UserPerson> GetMessages(int groupId);
    public IEnumerable<UserPerson> GetMessages(int groupId, int page, int size);
    public IEnumerable<UserPerson> PostMessage(UserGroupMessage message);
    public IEnumerable<UserPerson> ConnectGroup(string group);
    public IEnumerable<UserPerson> DisconnectGroup(string group);
    
}

class GroupService: IGroupService
{
    public IEnumerable<string> GetMyGroups()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<UserGroup> GetGroups(int page, int size)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<UserGroup> SearchGroups(string query, int page, int size)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<UserPerson> GetPeople(int groupId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<UserPerson> GetMessages(int groupId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<UserPerson> GetMessages(int groupId, int page, int size)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<UserPerson> PostMessage(UserGroupMessage message)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<UserPerson> ConnectGroup(string group)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<UserPerson> DisconnectGroup(string group)
    {
        throw new NotImplementedException();
    }
}
public class UserGroupsService 
{
    private readonly AuthorizationDbContext _context;

    public UserGroupsService(AuthorizationDbContext context)
    {
        _context = context;
    }


    public int AddUserToGroup( int userId, int groupId )
    {
        var user = _context.UserContexts_.Find(userId);
        if (user is null)
            throw new ArgumentException("userId", $"Не найден пользователь с ид {userId}");
        var group = _context.UserGroups_.Find(groupId);
        if (group is null)
            throw new ArgumentException("groupId", $"Не найдена группа пользователей с ид {groupId}");
        if(_context.UserGroups_UserGroup.Any(ug => ug.UserId == userId && ug.GroupId == groupId))
        {
            throw new ArgumentException("userId,groupId", "Пользователь уже находится в этой группе");
        }
        UserGroups target = new UserGroups()
        {
            GroupId = groupId,
            UserId = userId
        };
        _context.UserGroups_UserGroup.Add(target);
        return _context.SaveChanges();
    }

    public List<UserGroupMessage> GetGroupMessages(int groupId, int page, int size)
    {
        return _context.UserGroupMessages_.Where(m => m.GroupId == groupId).OrderByDescending(m => m.Created).Skip((page - 1) * size).Take(size).ToList();
    }


    /*public List<UserRole> GetBusinessFunctions(int userId)
    {
        UserContext  user = _context.UserContexts_.Include(u => u.UserGroups).Where(u => u.Id == userId).SingleOrDefault();

        user.Groups = (from g in _context.UserGroups_ where (from p in user.UserGroups select p.GroupId).Contains(g.Id) select g).ToList();
        var userGroupIds = (from p in user.Groups select p.Id).ToList();
        return (from p in _context.GroupsBusinessFunctions.Include(b => b.BusinessFunction) where userGroupIds.Contains(p.GroupId) select p.BusinessFunction).ToList();
    }


    /// <summary>
    /// Получение списка сообщений, определённых протоколов входящей информации для бизнес функции
    /// </summary>
    /// <param name="function"></param>
    /// <returns></returns>
    public JArray GetMessagesForBusinessFunction( BusinessFunction function )
    {
        var input = function.Input;
        if(input == null)
        {
            throw new Exception("Входящая информация назначена");
        }
        using (var db = new AuthorizationDbContext())
        {
            string tableName = input.GetInputTableName();
            var p = db.GetDatabaseManager();
            TableManager dt = (TableManager)p.fasade[tableName];
            return dt.SelectAll();
        }
            
    }
    


    /// <summary>
    /// Получение параметров сообщений, источником которых является пользователь 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public List<MessageProtocol> GetMessageProtocolsForUser(int userId)
    {
     
        UserContext  user = _context.UserContexts_.Include(u=>u.UserGroups).Where(u=>u.Id==userId).SingleOrDefault();
        
        user.Groups = (from g in _context.UserGroups_ where (from p in user.UserGroups select p.GroupId).Contains(g.Id) select g).ToList();
        var userGroupIds = (from p in user.Groups select p.Id).ToList();
        var bsfs = (from p in _context.GroupsBusinessFunctions where userGroupIds.Contains(p.GroupId) select p.BusinessFunctionId).ToList();
        var protocols = (from p in _context.MessageProtocols.Include(p=>p.Properties) where p.FromId!=null && bsfs.Contains((int)p.FromId) select p).ToList();
        return protocols.ToList();
    }
    */
    public string GetUsername(int userId)
    {
        return (from p in _context.UserContexts_.Include(p => p.Person) where p.Id == userId select p.Person.FirstName + " " + p.Person.LastName + " " + p.Person.SurName).SingleOrDefault();
    }

    public UserGroup GetGroup(int id)
    {
        var group = _context.UserGroups_.Find(id);
        group.People = this.GetPersons(id);

        return group;
    }

    public List<UserGroup> GetGroups()
    {
        return _context.UserGroups_.ToList();
    }
       
    public List<UserGroup> GetUserGroups(int userId)
    {
        return _context.UserGroups_.Where(g => (from p in _context.UserGroups_UserGroup where p.UserId == userId select p.GroupId).Contains(g.Id)).ToList();
    }

    public List<UserPerson> GetPersons(int groupId)
    {
        return _context.UserContexts_.Include(ctx=>ctx.Person).Where(ctx=>_context.UserGroups_.Include(g=>g.UserGroups).First(g=>g.Id==groupId).UserGroups.Select(gr =>gr.UserId).Contains( ctx.Id)).Select(ctx=>ctx.Person).ToList();
    }

    public bool IsUserInGroup(int groupId, int userId)
    {
        return (from p in _context.UserGroups_UserGroup where p.UserId == userId && groupId == p.GroupId select p).SingleOrDefault() != null;
    }

    public void JoinToGroup(int groupId, int userId)
    {
        _context.UserGroups_UserGroup.Add(new UserGroups() { 
            GroupId=groupId,
            UserId = userId
        });
        _context.SaveChanges();
        //_notifications.Info($"Вы добавлены в группу: {GetGroup(groupId).Name }");
        PublishIntoGroup(userId, groupId, new UserMessage()
        {
            Created = DateTime.Now,
            Subject = "Состав группы",
            Text = $"Пользователь {GetUsername(userId)} вступил в группу"
        });
    }

    public void LeaveGroup(int groupId, int userId)
    {

        var connection = (from p in _context.UserGroups_UserGroup where p.UserId == userId && groupId == p.GroupId select p).SingleOrDefault();
        if(connection == null)
            throw new ArgumentException("groupId,userId");
        _context.UserGroups_UserGroup.Remove(connection);
        _context.SaveChanges();
        //_notifications.Info($"Вы покинули группу: {GetGroup(groupId).Name }");
            
        PublishIntoGroup(userId, groupId, new UserMessage() { 
            Created = DateTime.Now,
            Subject = "Состав группы",
            Text =    $"Пользователь {GetUsername(userId)} покинул группу"
        });
    }

    public void PublishIntoGroup(int userId, int groupId, UserMessage message)
    {
        UserGroupMessage newRecord = JsonConvert.DeserializeObject<UserGroupMessage>(JsonConvert.SerializeObject(message));
        newRecord.GroupId = groupId;
        _context.UserGroupMessages_.Add(newRecord);
        _context.SaveChanges();
        //_notifications.Info($"Вы успешно опубликовали сообщение в группе {GetGroup(groupId).Name}");
    }

    public void PublishIntoGroup(int userId, int groupId, UserGroupMessage message)
    {        
        message.GroupId = groupId;
        _context.UserGroupMessages_.Add(message);
        _context.SaveChanges();
        //_notifications.Info($"Вы успешно опубликовали сообщение в группе {GetGroup(groupId).Name}");
    }






}
 