
using System;
using System.Collections.Generic;
using System.Text;
public interface APIRegistration
{
    string Signup(UserAccount account,UserPerson person);
    void Signup(string Email, string Password, string Confirmation,
                string SurName, string FirstName, string LastName, DateTime Birthday, string Tel);
    bool HasUserWithEmail(string email);
    bool HasUserWithActivationKey(string activationKey);
    bool HasUserWithTel(string tel);
    UserContext  GetUserByEmail(string email);
    UserContext  GetUserByTel(string tel);
    string GetHashSha256(string password);
    string GenerateRandomPassword(int length);
    string GenerateActivationKey(int length);
    void RestorePasswordByEmail(string email);
}
