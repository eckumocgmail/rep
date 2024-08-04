using System;
using System.Collections.Concurrent;
using System.Linq;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

///
public sealed class SigninUser : BaseSignin<UserContext, UserAccount, UserPerson>
{
    public DbContextUser _model;
    private readonly APIActiveCollection<UserContext> _users;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SigninUser(IHttpContextAccessor http, DbContextUser model, AuthorizationUsers users, ITokenProvider tokenProvider): base(tokenProvider)
    {
        this._model = model;
        this._users = users;
        _httpContextAccessor = http;

    }

    public void UpdateUrlLocation(string url)
    {
        if(IsSignin())
        {
            using (var db = new DbContextUser())
            {
                var userId = Verify().Id;
                var user = db.UserContexts_.Find(userId);
                if(user is null)
                {
                    throw new Exception($"Пользователь с ид {userId} не найден в БД");
                }
                else
                {
                    user.URL = url;
                    db.SaveChanges();
                }                
            }
        }
    }

    public bool IsSignin()
    {
        string token = this._tokenProvider.Get();
        return String.IsNullOrWhiteSpace(token) ? false: this.Validate(token);
    }

    private UserContext GetBy(UserAccount item)
    {        
        var user = _model.UserContexts_.Include(ctx => ctx.Wallet).Include(ctx => ctx.Settings).Include(ctx => ctx.Person).Include(ctx => ctx.Account).Include(ctx => ctx.BusinessFunctions).FirstOrDefault(ctx => ctx.Account.Email.ToUpper() == item.Email.ToUpper());
        if(user is not null)
        {
            user.Roles = _model.UserRoles_.Where(r => user.BusinessFunctions.Select(bd => bd.RoleId).Contains(r.Id)).ToList();
            if (user.Roles is null)
                user.Roles = new();
        }
            
        return user;
    }

    public MethodResult<UserContext> SigninByLoginAndPassword(string email, string password)
    {
        return Signin(new UserAccount()
        {
            Email = email,
            Password = password
        });
    }

    public MethodResult<UserContext> Signin(string email, string password)
        => Signin(new UserAccount(email, password));
    public override MethodResult<UserContext> Signin(UserAccount item)
    {
        var user = GetBy(item);
        if( user == null)
            return MethodResult<UserContext>.OnError(new Exception("Пользователь не зарегистрирован"));
        if(user!=null && user.Account.Hash!=item.Hash)
            return MethodResult<UserContext>.OnError(new Exception("Пароль задан неверно"));
        if ((((AuthorizationUsers)_users).Has(user.SecretKey)))
        {
            _users.Remove(user.SecretKey);
        }
        var key = ((AuthorizationUsers)_users).GetByEmail(item.Email);
        if (key != null)
            _users.Remove(key);
        _tokenProvider.Set(user.SecretKey = _users.Put(user));     
        _model.UserContexts_.Find(user.Id).SecretKey = user.SecretKey;
        user.UserAgent += _httpContextAccessor.HttpContext.Request.Headers.UserAgent;
        user.LoginCount += 1;
        _model.SaveChanges();
        return MethodResult<UserContext>.OnResult(user);

    }
    private bool Compare(UserAccount stored, UserAccount input) => stored.Password == input.Password;

    public bool Signout() => Signout(_tokenProvider.Get());
    public override bool Signout(string key)
    {
        if (key != null)
        {
            var user = _users.Remove(key);
            _model.UserContexts_.Find(user.Id).IsActive = false;
            _model.SaveChanges();
        }
        return true;
    }
    public override bool Validate(string key)
    {
        var result = key != null && _users.GetMemory().ContainsKey(key);
        return result;
    }

    public UserContext Verify()
    {
        var token = this._tokenProvider.Get();
        return String.IsNullOrEmpty(token)? null: this._users.Take(token);
    }

    public List<string> GetNotifications()
    {
        if (IsSignin() == false)
        {
            return new List<string>();
        }
        else
        {
            ConcurrentQueue<string> notifications = GetFromSession<ConcurrentQueue<string>>("InputNotificationsQueue");
            var arr = notifications.ToList();
            notifications.Clear();
            return arr;
        }
    }

    public int PushNotifcation(string notification)
    {
        ConcurrentQueue<string> notifications = GetFromSession<ConcurrentQueue<string>>("InputNotificationsQueue");
        notifications.Enqueue(notification);

        return notifications.Count();
    }

    
    public override T GetFromSession<T>(string key)
    {
        var session = this._users.GetSession(this._tokenProvider.Get());
        if (session == null)
        {
            _httpContextAccessor.HttpContext.Response.Redirect("/Auth/Signin");
            throw new Exception("Сессия недоступна неавторизованному контексту")
            {


            };
        }
        else
        {
            if (session.ContainsKey(key) == false)
            {
                session[key] = typeof(T).New();
            }
            return (T)session[key];
        }
            
        
    }
}
