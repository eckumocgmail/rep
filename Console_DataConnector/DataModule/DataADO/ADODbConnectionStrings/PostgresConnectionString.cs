using Console_DataConnector.DataModule.DataADO.ADODbConnectorService;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataADO.ADODbConnectionStrings
{

    public class PostgresConnectionString : MyValidatableObject<PostgresDbConnector>
    {

        [Display(Name = "Сервер")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Host { get; set; }

        [Display(Name = "Порт")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int Port { get; set; }


        [Display(Name = "База данных")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Database { get; set; }




        [Display(Name = "Пользователь")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string UserId { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Password { get; set; }

        //Host=127.0.0.1;Port=5432;User Id=postgres;Password=sgdf1423;Database=postgres
        public PostgresConnectionString() : this("127.0.0.1", 5432, "postgres", "postgres", "sgdf1423")
        {
        }

        public PostgresConnectionString(string host, int port, string database, string userId, string password)
        {
            Host = host;
            Port = port;
            Database = database;
            UserId = userId;
            Password = password;
        }

        public override string ToString()
        {
            return $"Host={Host};Port={Port};User Id={UserId};Password={Password};Database={Database}";
        }
    }
}
