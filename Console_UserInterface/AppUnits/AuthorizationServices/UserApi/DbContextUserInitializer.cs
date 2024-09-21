
public class DbContextUserInitializer
{
    public IDictionary<string, int> Init(DbContextUser context, params string[] roles)
    {
        var result = new Dictionary<string, int>();
        result["UserRoles"] = InitRoles(context, roles);
        result["UserGroups"] = InitGroups(context, roles);
        return result;
    }

    public int InitRoles(DbContextUser context, string[] roles)
    {
        if (context.UserRoles_.Count() == 0)
        {
            context.AddRange(roles.Select(name => new UserRole()
            {
                Name = name,
                Description = name,
                Code = name
            }));
        }
        return context.SaveChanges();
    }

    public int InitGroups(DbContextUser context, string[] roles)
    {
        if (context.UserGroups_.Count() == 0)
        {
            context.AddRange(roles.Select(name => new UserGroup()
            {
                Name = name,
                Description = name,
                Code = name
            }));
        }        
        return context.SaveChanges();
    }

    public static void CreateUserData(IServiceCollection services, ConfigurationManager configuration)
    {
        using (var db = new DbContextUser())
        {
            var initializer = new DbContextUserInitializer();
            db.Info(initializer.Init(db, new string[] { "webuser", "transport", "holder", "customer" }).ToJsonOnScreen());
        }
    }
}
