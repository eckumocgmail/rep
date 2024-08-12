using System;

using Console_AuthModel.AuthorizationModel.UserModel;

using Microsoft.EntityFrameworkCore;

using static AuthorizationDbContext;
public class DbContextUser: DbContext {
    public virtual DbSet<UserAccount> UserAccounts_ { get; set; }
    public virtual DbSet<UserContext> UserContexts_ { get; set; }
    public virtual DbSet<UserWallet> UserWallets_ { get; set; }
    public virtual DbSet<TransferTransaction> TransferTransactions_ { get; set; }
    

    public virtual DbSet<UsersRoles> UsersRoles_ { get; set; }
    public virtual DbSet<UserRole> UserRoles_ { get; set; }
    public virtual DbSet<UserLogin> UserLogins_ { get; set; }
    public virtual DbSet<UserGroups> UserGroups_UserGroup { get; set; }
    public virtual DbSet<UserGroupMessage> UserGroupMessages_ { get; set; }
    public virtual DbSet<UserGroup> UserGroups_ { get; set; }

    public virtual DbSet<UserMessage> UserMessages_ { get; set; }
    public virtual DbSet<UserMessageFile> UserMessageFiles { get; set; }
    public virtual DbSet<UserPerson> UserPersons_ { get; set; }
    public virtual DbSet<UserSettings> UserSettings_ { get; set; }
 
    
 
    public DbContextUser(){}
    public DbContextUser(DbContextOptions<DbContextUser> options) : base(options){}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer($@"Data Source=DESKTOP-IHJM9RD;Initial Catalog={GetType().GetTypeName()};Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False" );
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
               .HasIndex(u => new { u.UserID, u.GroupID })
               .IsUnique();

        //uniq constraint
        builder.Entity<UserPerson>()
               .HasIndex(u => new { u.FirstName, u.SurName, u.LastName, u.Birthday })
               .IsUnique();
    }
}