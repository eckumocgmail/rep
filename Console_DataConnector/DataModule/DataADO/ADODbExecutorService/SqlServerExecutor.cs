﻿using Console_DataConnector.DataModule.DataADO.ADODbConnectorService;
using DataCommon;

using Microsoft.Data.SqlClient;

using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataADO.ADODbExecutorService
{
    public class SqlServerExecutor : SqlServerDbConnector, ISqlExecutor

    {
        protected readonly IDataTableService DataTableService = new DataTableService();

        public SqlServerExecutor()
        {
        }

        public SqlServerExecutor(string server, string database) : base(server, database)
        {
        }

        public SqlServerExecutor(string server, string database, bool trustedConnection, string userID, string password) : base(server, database, trustedConnection, userID, password)
        {
        }

        public int ExecuteProcedure(
            [InputEngText][NotNullNotEmpty()] string name, IDictionary<string, string> input, IDictionary<string, string> output)
        {
            try
            {               
                MethodBase.GetCurrentMethod().EnsureIsValid(new object[] { name, input, output});


                Console.WriteLine($"ExecuteProcedure({name})");
                SqlCommand command = new SqlCommand(name, GetConnection());
                command.CommandType = CommandType.StoredProcedure;

                command.ExecuteReader();
                command.Parameters.Add(new SqlParameter());
                return 1;
            }
            catch(Exception ex)
            {
                this.Error(ex.ToDocument());
                throw new Exception($"Не удалось выполнить процедуру {name}\nс параметрами\n{input.ToJsonOnScreen()}", ex);
            }
        }

        public JArray GetJsonResult(string sql)
        {
            Console.WriteLine($"GetJsonResult({sql})");
            DataTable ResultDataTable = ExecuteQuery(sql);
            JArray JResult = DataTableService.GetJArray(ResultDataTable);
            return JResult;
        }

        public JObject GetSingleJObject(string sql)
        {
            Console.WriteLine($"GetSingleJObject({sql})");
            DataTable ResultDataTable = ExecuteQuery(sql);
            JArray JResult = DataTableService.GetJArray(ResultDataTable);
            JToken token = JResult.FirstOrDefault();
            return token != null ? (JObject)token : null;
        }


        /// <summary>
        /// Выполнение запроса с 1 результирующим набором
        /// </summary>
        public DataTable ExecuteQuery(string tsql)
        {
            Console.WriteLine($"ExecuteQuery({tsql})");
            DataTable dataTable = new DataTable();
            Console.WriteLine($"{tsql}=>{dataTable.Rows.Count}");
            SqlCommand command = new SqlCommand(tsql, GetConnection());
            using (SqlDataAdapter adapter = new SqlDataAdapter(tsql, GetConnection()))
            {
                adapter.Fill(dataTable);
            }
            return dataTable;
        }

        /// <summary>
        /// Выполнение простой команды (без результирующего набора)
        /// </summary>
        public int PrepareQuery(string tsql)
        {
            Console.WriteLine($"PrepareQuery({tsql})");
            try
            {
                SqlCommand command = new SqlCommand(tsql, GetConnection());
                int result = command.ExecuteNonQuery();
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception($"Не удалось выполнить команду " + tsql + " \n" + ex.Message);
            }
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string command) where TEntity : class
        {
            return DataTableService.GetResultSet<TEntity>(ExecuteQuery(command));
        }

        public int TryPrepareQuery(string command)
        {
            try
            {
                return PrepareQuery(command);
            }
            catch (Exception ex)
            {
                this.Error(ex);
                return 0;
            }
        }

        public IEnumerable<dynamic> ExecuteQuery(string command, Type entity)
        {
            return DataTableService.GetResultSet(ExecuteQuery(command), entity);
        }
    }
}
