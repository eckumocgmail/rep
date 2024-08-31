using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataADO.ADODbConnectionStrings
{
    /// <summary>
    /// Модель параметров подключения к источнику данных ADO MySQL
    /// </summary>
    public class MySqlConnectionString : MyValidatableObject
    {

        [Display(Name = "Сервер")]
        [Required(ErrorMessage = "Необходимо ввести наименование источника")]
        public string Server { get; set; }


        [Display(Name = "Пользователь")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string UserId { get; set; }


        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Password { get; set; }


        [Display(Name = "Порт")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int Port { get; set; }


        [Display(Name = "База данных")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Database { get; set; }


        public MySqlConnectionString() : this("localhost", 3306, "mysql", "root", "sgdf1423")
        {
        }

        public MySqlConnectionString(string server, int port, string database, string userId, string password)
        {
            Server = server;
            UserId = userId;
            Password = password;
            Port = port;
            Database = database;
        }

        /// <summary>
        /// Получение строки соединения ADO.NET
        /// </summary>
        /// <returns> строка соединения </returns>
        public override string ToString()
        {
            return $"Server={Server};Port={Port};Database={Database};User Id={UserId};Password={Password};PersistSecurityInfo=True;CharSet=utf8;SslMode=none";
        }
    }
}
