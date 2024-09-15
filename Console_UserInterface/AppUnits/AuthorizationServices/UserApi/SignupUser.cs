using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Console_AuthModel.AuthorizationModel.UserModel;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using pickpoint_delivery_service;

using static AuthorizationDbContext;

public sealed class SignupUser : BaseSignup<UserContext, UserAccount, UserPerson>
{
    private const string WebUserRoleCode = "customer";
    public DbContextUser _model;

    private readonly IHttpContextAccessor httpContextAccessor;
    public SignupUser(IHttpContextAccessor http, DbContextUser model)
    {
        this._model = model;
        this.httpContextAccessor = http;


    }

    public override bool Compare(UserAccount stored, UserAccount input) => stored.Password == input.Password;

    public override UserContext GetBy(UserAccount item)    
        => _model.UserContexts_.Include(ctx => ctx.Account).FirstOrDefault(ctx => ctx.Account.Email.ToUpper() == item.Email.ToUpper());

    public async Task<List<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
    {
        await Task.CompletedTask;
        throw new Exception();
    }

    public override bool HasWith(UserAccount item)
    {
        return _model.UserAccounts_.Where(ctx => ctx.Email.ToUpper() == item.Email.ToUpper()).Count() > 0;        
    }
   
    public MethodResult<UserContext> SignupCustomer(UserAccount item, UserPerson person)
    {
        try
        {
            if (HasWith(item))
                return MethodResult<UserContext>.OnError(new Exception("Пользователь с таким email уже зарегистрирован"));
            //item.Hash = Encoding.Unicode.GetString(SHA512.HashData(item.Password.ToCharArray().Select(ch => (byte)ch).ToArray()));
            var context = new UserContext();
            _model.Add(context.Account = item);

            var webUserRole = _model.UserRoles_.FirstOrDefault( role => role.Code == WebUserRoleCode );
            if(webUserRole is null)
            {
                _model.UserRoles_.Add(webUserRole = new UserRole()
                {
                    Code = "webuser",
                    Description = "Пользователь интернет магазина" 
                

                });
                _model.SaveChanges();
            }

            context.AddBusinessFunctions(webUserRole);
            _model.SaveChanges();

            //_model.Add(context.Role = new BusinessResource());
            _model.Add(context.Person = person);
            _model.SaveChanges();
            _model.Add(context.Wallet = new UserWallet());
            _model.SaveChanges();
            _model.Add(context.Settings = new UserSettings());
            _model.SaveChanges();
            _model.Add(context);
            _model.SaveChanges();
            return MethodResult<UserContext>.OnResult(context);
        }
        catch (DbUpdateException DbUpdateException)
        {
            this.Error(DbUpdateException);
            return MethodResult<UserContext>.OnError(DbUpdateException.InnerException);
        }
    }
 
    public override MethodResult<UserContext> Signup(UserAccount item, UserPerson person) {
        return Signup(item.Email, item.Password, person.SurName, person.FirstName, person.LastName, ((DateTime)person.Birthday) , person.Tel);
    }


    public MethodResult<UserContext> Signup(

        [InputEmail("Электронный адрес задан некорректно")]
        [Label("Электронный адрес")]
        [NotNullNotEmpty("Не указан электронный адрес")]
        [Icon("email")]
        [UniqValue("Этот адрес электронной почты уже зарегистрирован")]
        [JsonProperty("Email")]
        string Email,

          
        string Password,

        [NotNullNotEmpty("Не указана фамилия пользователя")]
        [InputRusText("Записывайте фамилию кирилицей")]
        [Icon("person")]
        string SurName ,

        [Label("Имя")]
        [NotNullNotEmpty("Не указано имя пользователя")]
        [InputRusText("Записывайте имя кирилицей")]
        [Icon("person")]
        string FirstName,

        [InputOrder(3)]
        [Label("Отчество")]
        [NotNullNotEmpty("Не указано отчество пользователя")]
        [InputRusText("Записывайте отчество кирилицей")]
        [Icon("person")]
        string LastName ,

        [InputOrder(4)]
        [Label("Дата рождения")]
        [InputDate()]
        [NotNullNotEmpty("Не указана дата рождения пользователя")]
        [Icon("person")]
        DateTime? Birthday,

   
        [InputPhone("Номер телефона указан неверно")]
        [UniqValue("Этот номер телефона уже зарегистрирован")]
        [Label("Номер телефона")]
        [NotNullNotEmpty("Не указана номер телефона")]
        [Icon("phone")]
        string Tel)
    {
        try
        {
            if (_model.UserAccounts_.Where(ctx => ctx.Email.ToUpper() == Email.ToUpper()).Count() > 0)
                return MethodResult<UserContext>.OnError(new Exception("Пользователь с таким email уже зарегистрирован"));
            if (_model.UserPersons_.Where(ctx => ctx.Tel.ToUpper().Substring(ctx.Tel.Length - 10) == Tel.ToUpper().Substring(Tel.Length - 10)).Count() > 0)
                return MethodResult<UserContext>.OnError(new Exception("Пользователь с таким номером телефона уже зарегистрирован"));
            if (_model.UserPersons_.Where(ctx => ctx.LastName.Trim().ToLower() == LastName.Trim().ToLower() && ctx.FirstName.Trim().ToLower() == FirstName.Trim().ToLower() && ctx.SurName.Trim().ToLower() == SurName.Trim().ToLower() ).Count() > 0)
                return MethodResult<UserContext>.OnError(new Exception("Пользователь с таким именем уже зарегистрирован"));
            try
            {
                var context = new UserContext()
                {
                    Ip4 = "192.168.0.1",
                    UserAgent = httpContextAccessor.HttpContext.Request.Headers.UserAgent,
                    URL = httpContextAccessor.HttpContext.Request.GetDisplayUrl()
                };
                _model.Add(context.Wallet = new( ) { });
                _model.SaveChanges();
                _model.Add(context.Account = new(Email, Password) { });
                _model.SaveChanges();
                _model.Add(context.Person = new() { FirstName = FirstName, LastName = LastName, SurName = SurName, Birthday = Birthday, Tel = Tel });
                _model.SaveChanges();
                _model.Add(context.Settings = new UserSettings());
                _model.SaveChanges();
                _model.Add(context);
                _model.SaveChanges();

                Dictionary<string, List<string>> validaiton = new Dictionary<string, List<string>>();
                context.Validate().ToList().ForEach(kv => validaiton[kv.Key]=kv.Value) ;                
                context.Account.Validate().ToList().ForEach(kv => validaiton[kv.Key] = kv.Value);
                context.Person.Validate().ToList().ForEach(kv => validaiton[kv.Key] = kv.Value);
                context.Settings.Validate().ToList().ForEach(kv => validaiton[kv.Key] = kv.Value);
                if(validaiton.Count()>0)
                {
                    
                    return MethodResult<UserContext>.OnError($"Данные не валидны: {validaiton.ToJsonOnScreen()}");
                }

                _model.SaveChanges();
                return MethodResult<UserContext>.OnResult(context);
            }
            catch(Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                string message = ex.Message;
                foreach(var next in ex.Entries)
                {
                    message += "\n" + next;
                }
                return MethodResult<UserContext>.OnError(message);
            }
         
            
        }
        catch(DbUpdateException DbUpdateException)
        {
            this.Error(DbUpdateException);
            return MethodResult<UserContext>.OnError(DbUpdateException.InnerException);
        }
    }

    public async Task<SigninResult> PasswordSignInAsync(string email, string password, bool rememberMe, bool lockoutOnFailure)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    public MethodResult<UserContext> SignupRoles(UserAccount account, UserPerson person, string[] roles)
    {
        var result = Signup(account, person);

        if (result.Succeeded)
        {

            result.Result.BusinessFunctions = new();
            result.Result.Roles = roles.Select(name =>
            {
                var role = _model.UserRoles_.FirstOrDefault(r => r.Code.ToLower() == name);
                if (role == null)
                {
                    _model.UserRoles_.Add(role = new()
                    {
                        Code = name.ToLower(),
                        Name = name.ToLower()
                    });
                    _model.SaveChanges();
                }
                UsersRoles p = null;
                _model.UsersRoles_.Add(p = new()
                {
                    RoleId = role.Id,
                    UserId = result.Result.Id
                });
                result.Result.BusinessFunctions.Add(p);
                _model.SaveChanges();
                return role;
            }).ToList();
            _model.SaveChanges();

            using(var db=new DeliveryDbContext())
            {
                foreach (var role in roles)
                {
                    switch (role.ToLower())
                    {
                        case "holder":
                            db.Holders.Add(new Console_BlazorApp.AppUnits.DeliveryModel.Holder() { UserId = result.Result.Id });
                            break;
                        case "transport":
                            db.Transports.Add(new Console_BlazorApp.AppUnits.DeliveryModel.Transport() { UserId = result.Result.Id });
                            break;
                        case "customer":
                            db.Customers.Add(new Console_BlazorApp.AppUnits.DeliveryModel.CustomerContext() { UserId = result.Result.Id });
                            break;
                    }
                }
            }
            result.Result.BusinessFunctions = _model.UsersRoles_.Where(ur => ur.UserId == result.Result.Id).ToList();

        }
        return result;
    }

    public void RemoveWith(UserAccount account)
    {
        var user = GetBy(account);
        _model.UserAccounts_.Remove(_model.UserAccounts_.Find(user.AccountId));
        _model.UserPersons_.Remove(_model.UserPersons_.Find(user.PersonId));
        _model.UserSettings_.Remove(_model.UserSettings_.Find(user.SettingsId));
        _model.UserContexts_.Remove(user);
        _model.SaveChanges();
    }
}
