using Console_BlazorApp.AppUnits.DeliveryModel;

using Microsoft.EntityFrameworkCore;

public class UserMessagesService : IUserMessagesService
{
    private readonly DbContextUser _context;
    private readonly SigninUser _signin;

    public UserMessagesService(DbContextUser context, SigninUser signin)
    {
        _context = context;
        _signin = signin;
    }


    public async Task<int> Send(UserMessage p, List<ProductImage> files)
    {
        await Task.CompletedTask;
        p.EnsureIsValide();

        _context.Add(p);
        _context.SaveChanges();
        return 1;      
    }


    public List<UserMessage> GetInbox()
    {
        if (_signin.IsSignin() == false)
            throw new UnauthorizedAccessException("Метод доступен только авторизованным пользователям");
        int userId = _signin.Verify().Id;
        return (from p in _context.UserMessages_ where p.ToUserId == userId select p).ToList();
    }

    public List<UserMessage> GetOutbox()
    {
        if (_signin.IsSignin() == false)
            throw new UnauthorizedAccessException("Метод доступен только авторизованным п6ользователям");
        int userId = _signin.Verify().Id;
        return (from p in _context.UserMessages_ where p.FromUserId == userId select p).ToList();
    }

    public Dictionary<string, int> GetUsers()
    {
        var results = new Dictionary<string, int>(_context.UserContexts_.Include(u => u.Person).Select(u => new KeyValuePair<string, int>("#" + u.Id + " " + u.Person.GetFullName(), u.Id)));
        return results;
    }

    public int GetInboxNotReaded()
    {
        if (_signin.IsSignin() == false)
            throw new UnauthorizedAccessException("Метод доступен только авторизованным п6ользователям");
        int userId = _signin.Verify().Id;
        return (from p in _context.UserMessages_ where p.FromUserId == userId && p.Readed == false select p).Count();
    }
}

