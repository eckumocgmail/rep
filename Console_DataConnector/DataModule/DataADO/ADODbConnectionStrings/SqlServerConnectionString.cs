using System;
using System.ComponentModel.DataAnnotations;

namespace Console_DataConnector.DataModule.DataADO.ADODbConnectionStrings
{
    public class SqlServerConnectionString : MyValidatableObject, IDisposable
    {

        [Display(Name = "Сервер")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Server { get; set; }

        [Display(Name = "База данных")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Database { get; set; }

        [Display(Name = "Проверка подлинности Window")]
        [Required(ErrorMessage = "Обязательное поле")]
        public bool TrustedConnection { get; set; } = true;


        [Display(Name = "Пользователь")]
        public string UserID { get; set; }

        [Display(Name = "Пароль")]
        public string Password { get; set; }

        public SqlServerConnectionString() : this("DESKTOP-IHJM9RD", "www") { }
        public SqlServerConnectionString(string server, string database) : this(server, database, true, "", "") { }
        public SqlServerConnectionString(string server, string database, bool trustedConnection, string userID, string password)
        {
            Server = server;
            Database = database;
            TrustedConnection = trustedConnection;
            UserID = userID;
            Password = password;
        }

        public override string ToString()
        {
            //"Data Source=DESKTOP-IHJM9RD;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
            string constr = $"Data Source={Server};Initial Catalog={Database};";
            if (TrustedConnection)
            {
                constr = constr + "Integrated Security=True;Trust Server Certificate=False;MultipleActiveResultSets=true;Encrypt=False;";
            }
            else
            {
                constr += $"UID={UserID};PWD={Password};Trust Server Certificate=False;MultipleActiveResultSets=true;Encrypt=False;";
            }
            return constr;
        }

        public void Dispose()
        {
        }
    }

}