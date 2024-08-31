

using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public class RegistrationService : APIRegistration
{
    private AuthorizationDbContext _db;
    private readonly MailRuService2 _email;
    private readonly AuthorizationOptions _options;

    public RegistrationService(AuthorizationDbContext db, AuthorizationOptions options, MailRuService2 email)
    {
        _db = db;
        _email = email;
        _options = options;
    }


    /// <summary>
    /// Восстановление пароля по электронной почте
    /// </summary>
    /// <param name="email"></param>
    public void RestorePasswordByEmail(string email)
    {
        UserContext user = this.GetUserByEmail(email);
        if (user == null)
        {
            throw new Exception("Пользователь с таким адресом электронной почты не зарегистрирован");                
        }
        string password = GenerateRandomPassword(10);
        _db.UserAccounts_.Find(user.Account.Id).Hash = GetHashSha256(password);
        _db.SaveChanges();
        _email.Send(email, "Восстановление пароля", "Пароль от Вашей учетной записи: " + password);
    }

    /// <summary>
    /// Проверка регистрации пользователя с заданным электронным адресом
    /// </summary>
    /// <param name="Email">электронный адрес</param>
    /// <returns></returns>
    public bool HasUserWithEmail(string Email)
    {
        return (from user
                            in _db.UserContexts_.Include(a => a.Account)
                where user.Account.Email == Email
                select user).Count() > 0;

    }


    /// <summary>
    /// Проверка наличия пользователя с заданным номером телефона
    /// </summary>
    /// <param name="tel">номер телефона в формате x-xxx-xxx-xxxx </param>
    /// <returns></returns>
    public bool HasUserWithTel(string tel)
    {
        return (from user
                            in _db.UserContexts_.Include(a => a.Account)
                where user.Person.Tel == tel
                select user).Count() > 0;

    }


    /// <summary>
    /// Получение данных пользователя по адресу электронной почты,
    /// данный метод регистрозависимый, т.е для поиска нужно указать адрес электронной почты 
    /// в том же регистре в котором он был зарегистрирован.
    /// </summary>
    /// <param name="email">адрес электронной почты</param>
    /// <returns></returns>
    public UserContext GetUserByEmail(string email)
    {

        UserAccount account = (from p in _db.UserAccounts_ where p.Email == email select p).SingleOrDefault();
        if (account == null)
        {
            return null;
        }
        else
        {
            return (from p in _db.UserContexts_
                                .Include(a => a.Account)
                                .Include(a => a.Settings)
                                .Include(a => a.Person)
                                //.Include(a => a.Role)
                                .Include(a => a.UserGroups)
                    where p.AccountId == account.Id select p).SingleOrDefault();
        }

    }


    /// <summary>
    /// Получение данных пользователя по номеру телефона, номер телефона регистрируется в формате 7-XXX-XXX-XXXX
    /// </summary>
    /// <param name="tel">номер телефона</param>
    /// <returns></returns>
    public UserContext GetUserByTel(string tel)
    {
        UserPerson person = (from p in _db.UserPersons_ where p.Tel == tel select p).SingleOrDefault();
        if (person == null)
        {
            return null;
        }
        else
        {
            return (from p in _db.UserContexts_
                                .Include(a => a.Account)
                                .Include(a => a.Settings)
                                .Include(a => a.Person)
                                //.Include(a => a.Role)
                                .Include(a => a.UserGroups)
                    where p.PersonId == person.Id
                    select p).SingleOrDefault();
        }
    }


    /// <summary>
    /// Метод применения функции хеширования символов
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public string GetHashSha256(string password)
    {
        byte[] bytes = Encoding.Unicode.GetBytes(password);
#pragma warning disable SYSLIB0021 // Тип или член устарел
        SHA256Managed hashstring = new SHA256Managed();
#pragma warning restore SYSLIB0021 // Тип или член устарел
        byte[] hash = hashstring.ComputeHash(bytes);
        string hashString = string.Empty;
        foreach (byte x in hash)
        {
            hashString += String.Format("{0:x2}", x);
        }
        return hashString;
    }


    /// <summary>
    /// Метод генерации случайного пароля заданной длины
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public string GenerateRandomPassword(int length)
    {
        Random random = new Random();
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                        "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower() +
                        "0123456789";
        return new string(Enumerable.Repeat(chars, length)
                            .Select(s => s[random.Next(s.Length)]).ToArray());

    }


    /// <summary>
    /// Проверка наличия пользователя с зарегистриваронным ключом активации
    /// </summary>
    /// <param name="activationKey">ключ актвации</param>
    /// <returns>true, если такой ключ уже зарегистрирован</returns>
    public bool HasUserWithActivationKey(string activationKey)
    {
        UserAccount account = (from p in _db.UserAccounts_ where p.ActivationKey == activationKey select p).SingleOrDefault();
        return account != null;
    }


    /// <summary>
    /// Генерация уникального ключа активации учетной записи
    /// </summary>
    /// <param name="length">длина ключа</param>
    /// <returns></returns>
    public string GenerateActivationKey(int length)
    {
        string key = null;
        do
        {
            key = RandomString(length);
        } while (this.HasUserWithActivationKey(key));
        return key;
    }

    /// <summary>
    /// Получение случайной последовательности символов
    /// </summary>
    /// <returns> последовательность символов </returns>
    private string RandomString(int keylength)
    {
        Random random = new Random();
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                        "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower() +
                        "0123456789";
        return new string(Enumerable.Repeat(chars, keylength)
                            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    public void Signup(UserAccount account, UserPerson person)
    {
        Signup(person,account);
    }

    /// <summary>
    /// Регистрация пользователя в системе
    /// </summary>
    /// <param name="Email">электронный адрес</param>
    /// <param name="Password">пароль</param>
    /// <param name="Confirmation">подтверждение</param>
    public void Signup(UserPerson person, UserAccount account)
    {
        UserSettings settings = new UserSettings();
        //BusinessResource role = (from r in _db.Roles where r.Code == _options.PublicRole select r).SingleOrDefault();
        //Group group = (from g in _db.Groups where g.Name == _options.PublicGroup select g).SingleOrDefault();
        UserContext user = new UserContext()
        {
            Person = person,
            Account = account,
            Settings = settings,
            //Role = role,
            LastActive = GetTimestamp(),
            LoginCount = 0,
            IsActive = false
        };


        _db.UserPersons_.Add(person);
        _db.UserAccounts_.Add(account);
        _db.UserSettings_.Add(settings);
        _db.UserContexts_.Add(user);
        _db.SaveChanges();
    }
    public void Signup(string Email, string Password, string Confirmation, string SurName, string FirstName, string LastName, DateTime Birthday, string Tel)
    {

        UserAccount account = new UserAccount(Email, Password)
        {
        
        };
        UserPerson person = new UserPerson()
        {
            SurName = SurName,
            FirstName = FirstName,
            LastName = LastName,
            Birthday = Birthday,
            Tel = Tel
        };
        Signup(person,account);
    }

    /// <summary>
    /// Получечние текущего времени в милисекундах
    /// </summary>
    /// <returns></returns>
    private long GetTimestamp()
    {
        return (long)(((DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0))).TotalMilliseconds);
    }


    UserContext APIRegistration.GetUserByEmail(string email)
    {
        return _db.UserContexts_.Include(u => u.Account).FirstOrDefault(u => u.Account.Email.ToLower() == email.ToLower());
    }

    UserContext APIRegistration.GetUserByTel(string tel)
    {
        return _db.UserContexts_.Include(u => u.Person).Include(u => u.Account).FirstOrDefault(u => u.Person.Tel.ToLower() == tel.ToLower());
    }

    string APIRegistration.Signup(UserAccount account, UserPerson person)
    {
        UserSettings settings = new UserSettings();
        //BusinessResource role = (from r in _db.Roles where r.Code == _options.PublicRole select r).SingleOrDefault();
        //Group group = (from g in _db.Groups where g.Name == _options.PublicGroup select g).SingleOrDefault();
        UserContext user = new UserContext()
        {
            Person = person,
            Account = account,
            Settings = settings,
            //Role = role,
            LastActive = GetTimestamp(),
            LoginCount = 0,
            IsActive = false
        };


        _db.UserPersons_.Add(person);
        _db.UserAccounts_.Add(account);
        _db.UserSettings_.Add(settings);
        _db.UserContexts_.Add(user);
        _db.SaveChanges();
        return null;
    }
}
