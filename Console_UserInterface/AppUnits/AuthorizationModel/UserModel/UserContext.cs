using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;


/// <summary>
/// Обьект модели пользователя сеансов
/// </summary>
[Label("Пользователь")]      
[Icon("build")]
public class UserContext : ActiveObject
{

    public ClaimsPrincipal ToClaimsPrincipal()
    {
        var claims = new Claim[]
        {
            //new Claim(ClaimTypes.NameIdentifier, Account is not null? Account.Email: ""),
            //new Claim(ClaimTypes.Name, Account?.Email),
            //new Claim(ClaimTypes.Hash, Account?.Hash),
        };
        var roles = (Roles is null ? new List<UserRole>() : Roles).Select(r => new Claim(ClaimTypes.Role, r.Code));
        
        var identity = new ClaimsIdentity(claims.Concat(roles) );
        return new ClaimsPrincipal(identity);
    }

    public static UserContext FromClaimsPrincipal(ClaimsPrincipal principal) => new()
    {
        Account = new UserAccount()
        {
            Email = principal.FindFirst(ClaimTypes.Name)?.Value ?? "",
            Password = principal.FindFirst(ClaimTypes.Hash)?.Value ?? "",
        },
        
      
        Roles = principal.FindAll(ClaimTypes.Role).Select(c => new UserRole()
        {
            Code = c.Value
        }).ToList()
    };
    [Label("Учетная запись")]
    public int AccountId { get; set; }

    [InputHidden(true)]
    [Label("Учетная запись")]
    public virtual UserAccount Account { get; set; }

    public int WalletId { get; set; }
    public UserWallet Wallet { get; set; }
    

    //[Label("Роль")]
    //public int? BusinessResourceId { get; set; }

    //[InputHidden(true)]
    //[Label("Роль")]
    //public virtual BusinessResource Role { get; set; }


    [Label("Настройки")]
    public int SettingsId { get; set; }
    [Label("Настройки")]
    public virtual UserSettings Settings { get; set; }
            

    [Label("Личная инф.")]
    public int PersonId { get; set; }

    [Label("Личная инф.")]
    public virtual UserPerson Person { get; set; }


    [NotMapped]
    [Label("Группы")]
    public virtual List<UserGroup> Groups { get; set; } = new List<UserGroup>();


    [Label("Группы")]
    [NotMapped]
    public int UserGroupsId { get; set; }

    [Label("Группы")]
    [ManyToMany("Groups")]
    [InputHidden(true)]
    public virtual List<UserGroups> UserGroups { get; set; } = new List<UserGroups>();


    [Label("Кол-во посещений")]
    public int LoginCount { get; set; }





    [Label("Входящие сообщения")]
    [InverseProperty("ToUser")]
    [NotMapped]

    public virtual List<UserMessage> Inbox { get; set; } = new List<UserMessage>();



    [Label("Исходящие сообщения")]
    [InverseProperty("FromUser")]
    [NotMapped]

    public virtual List<UserMessage> Outbox { get; set; } = new List<UserMessage>();


    
    public int? OrderId { get; set; }

 
    [Label("Выполняемые функции")]
    public virtual List<UsersRoles> BusinessFunctions { get; set; } = new List<UsersRoles>();
    [NotMapped]
    public virtual List<UserRole> Roles { get; set; } = new List<UserRole>();




    public string GetHomeUrl()
    {
        return "";// $"/{this.Role.Code}Face/{this.Role.Code}/{this.Role.Code}Home";
    }

    public UsersRoles AddBusinessFunctions(UserRole webUserRole)
    {
        UsersRoles r = null;
        BusinessFunctions.Add(r=new UsersRoles()
        {
            UserId = Id,
            RoleId = webUserRole.Id
        });
        return r;
    }
}

