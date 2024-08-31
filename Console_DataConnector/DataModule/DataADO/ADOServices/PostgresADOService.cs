﻿using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console_DataConnector.DataModule.DataApi;
using Console_DataConnector.DataModule.DataADO.ADODbConnectionStrings;

namespace Console_DataConnector.DataModule.DataADO.ADOServices
{


    /// <summary>
    /// Сервис выполнения sql-запросов 
    /// </summary>
    public class PostgresADOService : PostgresConnectionString, ISqlExecute
    {

        public string GetConnectionString() => base.ToString();

        public PostgresADOService() : base()
        {

            this.Info("Create");
        }



        /// <summary>
        /// Проверка доступности источника
        /// </summary>
        /// <param name="ConnectionString">строка соединения</param>
        /// <returns></returns>
        public bool CanConnect(string ConnectionString)
        {
            try
            {
                this.Info($"CanConnect({ConnectionString})");
                var tables = new List<string>();
                using (NpgsqlConnection con = new NpgsqlConnection(ConnectionString))
                {
                    NpgsqlCommand command = new NpgsqlCommand("SELECT version()", con);
                    con.Open();

                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string tableName = reader.GetString(0);
                        tables.Add(tableName);
                    }
                    reader.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                this.Info("Ошибка при попытки установить соединение");
                this.Error(ex);
                return false;
            }
        }


        /// <summary>
        /// Выполнение запроса на выборку данных
        /// </summary>
        /// <param name="ConnectionString">строка соединения</param>
        /// <param name="SQL">текст sql-запроса</param>
        /// <returns>результирующий набор</returns>
        public DataTable ExecuteQuery(string ConnectionString, string SQL)
        {
            this.Info($"ExecuteQuery({ConnectionString},{SQL})");
            using (NpgsqlConnection con = new NpgsqlConnection(ConnectionString))
            {
                NpgsqlCommand command = new NpgsqlCommand(SQL, con);
                con.Open();

                DataTable dataTable = new DataTable();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(SQL, con);
                adapter.Fill(dataTable);

                con.Close();
                return dataTable;
            }
        }


        /// <summary>
        /// Получение списка таблиц базы данных
        /// </summary>
        /// <param name="ConnectionString">строка соединения ADO с сервером БД</param>
        /// <returns>список таблиц</returns>
        public IEnumerable<string> GetTables(string ConnectionString)
        {
            this.Info($"GetTables({ConnectionString})");
            throw new NotImplementedException();
        }







        /// <summary>
        /// Выполнение SQL комманды 
        /// </summary>
        /// <param name="ConnectionString">строка соединения</param>
        /// <param name="SQL">текст sql-команды</param>   
        public int PrepareQuery(string ConnectionString, string SQL)
        {
            this.Info($"PrepareQuery({ConnectionString},{SQL})");
            using (NpgsqlConnection con = new NpgsqlConnection(ConnectionString))
            {
                NpgsqlCommand command = new NpgsqlCommand(SQL, con);
                con.Open();

                int result = command.ExecuteNonQuery();
                return result;
            }
        }

        public DataTable ExecuteQuery(string SQL) => ExecuteQuery(base.ToString(), SQL);
        public int PrepareQuery(string SQL) => PrepareQuery(base.ToString(), SQL);

        public DataTable ExecuteProcedure(string name, IDictionary<string, string> input, IDictionary<string, string> output)
        {
            throw new NotImplementedException();
        }
    }

}