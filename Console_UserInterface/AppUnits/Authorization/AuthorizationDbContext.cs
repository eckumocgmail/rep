using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Console_AuthModel.AuthorizationModel.UserModel;
/// <summary>
/// Контекст доступа к обьектам базы данных (EntityFramework)
/// </summary>
public partial class AuthorizationDbContext : DbContext 
{
    public class UsersRoles: BaseEntity
    {
        public int RoleId { get; set; }
        public UserRole Role { get; set; }

        public int UserId { get; set; }
        public UserContext User { get; set; }
    }

    public virtual DbSet<UserRole> UserRoles_ { get; set; }
    public virtual DbSet<UserAccount> UserAccounts_ { get; set; }
    public virtual DbSet<UserContext> UserContexts_ { get; set; }
 
    
    public virtual DbSet<UserLogin> UserLogins_ { get; set; }
    public virtual DbSet<UserGroups> UserGroups_UserGroup { get; set; }
    public virtual DbSet<UserGroupMessage> UserGroupMessages_ { get; set; }
    public virtual DbSet<UserGroup> UserGroups_ { get; set; }

    public virtual DbSet<UserMessage> UserMessages_ { get; set; }
    public virtual DbSet<UserPerson> UserPersons_ { get; set; }
    public virtual DbSet<UserSettings> UserSettings_ { get; set; }
 
    

    public int ExecuteProcedure<T>(string sql, object[] parameters)
    {    
        try
        {
              return Database.ExecuteSqlRaw (sql, parameters);
        }
        catch(Exception ex)
        {
              throw new Exception("Не удалось выполнить хранисую прцоедуру "+sql, ex);
        }
    }

    public AuthorizationDbContext(){
       
    }
    public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options) : base(options){}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Configure(optionsBuilder);
    }

    private static void Configure(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer(
                $@"Data Source=DESKTOP-IHJM9RD;Initial Catalog={nameof(AuthorizationDbContext)};Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        //uniq constraint
        builder.Entity<UserRole>()
               .HasIndex(u => u.Name)
               .IsUnique();

        //uniq constraint
        builder.Entity<UserRole>()
               .HasIndex(u => u.Code)
               .IsUnique();

        //uniq constraint
        builder.Entity<UserAccount>()
               .HasIndex(u => u.Email)
               .IsUnique();


        //uniq constraint
        builder.Entity<UserGroup>()
               .HasIndex(u => u.Name)
               .IsUnique();

        //uniq constraint
        builder.Entity<UserGroups>()
               .HasIndex(u => new { u.UserId, u.GroupId })
               .IsUnique();

        //uniq constraint
        builder.Entity<UserPerson>()
               .HasIndex(u => new { u.FirstName, u.SurName, u.LastName, u.Birthday })
               .IsUnique();
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        InputConsole.GetLogger<AuthorizationDbContext>().Info("Регистрация служб ... ");
        services.AddDbContext<AuthorizationDbContext>(options =>
            options.UseSqlServer($@"Data Source=DESKTOP-IHJM9RD;Initial Catalog={typeof(AuthorizationDbContext).GetTypeName()};Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));
        InputConsole.GetLogger<AuthorizationDbContext>().Info("Регистрация служб ... Успешно завершена");
        services.AddTransient<RegistrationService>();

    }
}