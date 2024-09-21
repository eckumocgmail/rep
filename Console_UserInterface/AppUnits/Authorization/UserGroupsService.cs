using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

/// <summary>
/// Методы работы с группами пользователей
/// </summary>
public interface IUserGroupsService
{
    UserGroup GetGroup(int id);
    List<UserGroup> GetGroups();
    List<UserGroup> GetUserGroups(int userId);
    List<UserPerson> GetPersons(int groupId);


    int AddUserToGroup(int userId, int groupId);
    List<UserGroupMessage> GetGroupMessages(int groupId, int page, int size);
    void PublishIntoGroup(int userId, int groupId, UserGroupMessage message);
    void PublishIntoGroup(int userId, int groupId, UserMessage message);

    

    bool IsUserInGroup(int groupId, int userId);
    void JoinToGroup(int groupId, int userId);
    void LeaveGroup(int groupId, int userId);

    string GetUsername(int userId);
}


/// <summary>
/// 
/// </summary>
public class UserGroupsService : IUserGroupsService
{
    private readonly DbContextUser _context;

    public UserGroupsService(DbContextUser context)
    {
        _context = context;
    }


    public int AddUserToGroup(int userId, int groupId)
    {
        var user = _context.UserContexts_.Find(userId);
        if (user is null)
            throw new ArgumentException("userId", $"Не найден пользователь с ид {userId}");
        var group = _context.UserGroups_.Find(groupId);
        if (group is null)
            throw new ArgumentException("groupId", $"Не найдена группа пользователей с ид {groupId}");
        if (_context.UserGroups_UserGroup.Any(ug => ug.UserId == userId && ug.GroupId == groupId))
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
        return _context.UserContexts_.Include(ctx => ctx.Person).Where(ctx => _context.UserGroups_.Include(g => g.UserGroups).First(g => g.Id == groupId).UserGroups.Select(gr => gr.UserId).Contains(ctx.Id)).Select(ctx => ctx.Person).ToList();
    }

    public bool IsUserInGroup(int groupId, int userId)
    {
        return (from p in _context.UserGroups_UserGroup where p.UserId == userId && groupId == p.GroupId select p).SingleOrDefault() != null;
    }

    public void JoinToGroup(int groupId, int userId)
    {
        _context.UserGroups_UserGroup.Add(new UserGroups()
        {
            GroupId = groupId,
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
        if (connection == null)
            throw new ArgumentException("groupId,userId");
        _context.UserGroups_UserGroup.Remove(connection);
        _context.SaveChanges();
        //_notifications.Info($"Вы покинули группу: {GetGroup(groupId).Name }");

        PublishIntoGroup(userId, groupId, new UserMessage()
        {
            Created = DateTime.Now,
            Subject = "Состав группы",
            Text = $"Пользователь {GetUsername(userId)} покинул группу"
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
 