using System.Collections.Concurrent;

namespace Console_UserInterface.Services
{

    /// <summary>
    /// 
    /// </summary>
    public class SesionModule
    {
        public static void AddSessionService( IServiceCollection services )
        {
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddSingleton<AppSessionContext>();
            services.AddSingleton<ISessionService, SessionService>();
        }
    }



    /// <summary>
    /// Singleton обеъкт содержащий все объекты сеансов
    /// </summary>
    public class AppSessionContext
    {
        public ConcurrentDictionary<string, Dictionary<string, object>> sessions = new();
    }


    /// <summary>
    /// Тестирование методов работы с сеансом
    /// </summary>
    public class SessionUnit: TestingElement
    {
        public SessionUnit(IServiceProvider parent) : base(parent)
        {
        }

        public override void OnTest()
        {
            this.AssertService<ISessionService>(sessions => {
                this.Info("Объекты сеанса:");
                this.Info(sessions.GetKeys().ToJsonOnScreen());
                sessions.SetValue<UserAccount>("account", new UserAccount("eckumoc@gmail.com","Gye*34FRtw"));
                var founded = sessions.GetValue<UserAccount>("account");                
                return founded is not null;
            },
            "Получение установка объектов сеанса работает", 
            "Получение установка объектов сеанса не работает");
        }          
    }


    /// <summary>
    /// РЕализация методов работы с сеансом
    /// </summary>
    public class SessionService : ISessionService
    {
        public AppSessionContext app { get; }

        public SessionService( AppSessionContext app)
        {
            this.app = app;
        }

        public Dictionary<string,object> CreateSession()
        {
            var key = GenerateKey();
            app.sessions[key]=new();
            SetToken(key);
            return app.sessions[key];
        }

        public string GenerateKey()
        {
            string key = null;
            do
            {
                key = RandomString();
            } while (this.app.sessions.ContainsKey(key));
            return key;
        }
        private string RandomString()
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                            "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower() +
                            "0123456789";
            return new string(Enumerable.Repeat(chars, 32)
                                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

         
        public Dictionary<string,object> GetSession(string key) => app.sessions.ContainsKey(key) ? app.sessions[key]: null;
        string token;
        public string GetToken() => this.token;

        public void SetToken(string token) => this.token = token;

        public List<string> GetKeys() => app.sessions.Keys.ToList();

        public object GetValue(string key)=> app.sessions[GetToken()][key];

        public void SetValue(string key, object p)
        {
            if(GetToken() is null)
            {
                CreateSession();
            }
            app.sessions[GetToken()][key] = p;
        }

        public void SetValue<T>(string key, T p)
        {
            if (GetToken() is null)
            {
                CreateSession();
            }
            app.sessions[GetToken()][key] = p;
        }


        public T GetValue<T>(string key) where T : class
        {
            if (GetToken() is null)
            {
                CreateSession();
            }
            return (T)(app.sessions[GetToken()].ContainsKey(key)? (T)app.sessions[GetToken()][key]: null);
        }
    }

    /// <summary>
    /// Методы работы с сеансом
    /// </summary>
    public interface ISessionService
    {
        Dictionary<string, object> CreateSession();
        string GenerateKey();
        Dictionary<string,object> GetSession(string key);
        string GetToken();
        void SetToken(string token);


        List<string> GetKeys();
        object GetValue(string key);
        void SetValue(string key, object p);
        void SetValue<T>(string key, T p);
        T GetValue<T>(string key) where T : class;
    }
}
