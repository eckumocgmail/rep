using Microsoft.EntityFrameworkCore;

using pickpoint_delivery_service;

using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Console_AuthModel.AuthorizationServices.AuthorizationApi
{
    public class AuthorizationService: APIAuthorization
    {
        private readonly SigninUser _user;
        private readonly DbContextUser authorizationDbContext;

        public AuthorizationService(SigninUser user, DbContextUser authorizationDbContext)
        {
            this._user = user;
            this.authorizationDbContext = authorizationDbContext;
        }

        public bool IsSignin()
        {
            return _user.IsSignin();
        }

        public bool InBusinessResource(string roleName)
        {
            return _user.Verify().BusinessFunctions.Any(function => authorizationDbContext.UserRoles_.Find(function.RoleId).Code.ToLower() == roleName.ToLower());
        }

        public bool IsActivated()
        {
            return _user.Verify().Account.Activated is not null;
        }

        public UserContext Signin(string RFIdLabel)
        {
            return authorizationDbContext.UserContexts_.Include(u => u.Account).FirstOrDefault(u => u.AccountId == authorizationDbContext.UserAccounts_.FirstOrDefault(user => user.RFId == RFIdLabel).Id);
        }

        public UserContext Signin(string Email, string Password, bool? IsFront = false)
        {
            return _user.SigninByLoginAndPassword(Email, Password).Result;
        }

        public void Signout(bool? IsFront = false)
        {
            _user.Signout();
        }

        public UserContext Verify(bool? IsFront = false)
        {
            throw new NotImplementedException();
        }

        public ConcurrentDictionary<string, object> Session()
        {
            throw new NotImplementedException();
        }

        public Task<UserAccount> GetAccountById(int iD)
        {
            throw new NotImplementedException();
        }

        public string GetUserHomeUrl()
        {
            throw new NotImplementedException();
        }

        public string Signup(UserAccount account, UserPerson person)
        {
            throw new NotImplementedException();
        }

        public void Signup(string Email, string Password, string Confirmation, string SurName, string FirstName, string LastName, DateTime Birthday, string Tel)
        {
            throw new NotImplementedException();
        }

        public bool HasUserWithEmail(string email)
        {
            throw new NotImplementedException();
        }

        public bool HasUserWithActivationKey(string activationKey)
        {
            throw new NotImplementedException();
        }

        public bool HasUserWithTel(string tel)
        {
            throw new NotImplementedException();
        }

        public UserContext GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public UserContext GetUserByTel(string tel)
        {
            throw new NotImplementedException();
        }

        public string GetHashSha256(string password)
        {
            throw new NotImplementedException();
        }

        public string GenerateRandomPassword(int length)
        {
            throw new NotImplementedException();
        }

        public string GenerateActivationKey(int length)
        {
            throw new NotImplementedException();
        }

        public void RestorePasswordByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
