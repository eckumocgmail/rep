using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Console_DataConnector.DataModule.DataADO.ADODbConnectionStrings;

namespace Console_DataConnector.DataModule.DataADO.ADODbConnectorService
{

    /// <summary>
    /// Сервис выполнения sql-запросов 
    /// </summary>
    public class SqlServerDbConnector : SqlServerConnectionString,
        IDbConnector<Microsoft.Data.SqlClient.SqlConnection>, IDisposable
    {
        private Microsoft.Data.SqlClient.SqlConnection _Connection;


        public SqlServerDbConnector() : base()
        {
            Console.WriteLine("Create");
        }

        public SqlServerDbConnector(string server, string database) : base(server, database)
        {
        }

        public SqlServerDbConnector(string server, string database, bool trustedConnection, string userID, string password) : base(server, database, trustedConnection, userID, password)
        {
        }

        public Microsoft.Data.SqlClient.SqlConnection GetConnection()
        {
            Console.WriteLine("GetConnection()");
            if (_Connection == null)
            {
                _Connection = CreateAndOpenConnection();
            }
            return _Connection;
        }


        public new void Dispose()
        {
            Console.WriteLine("Dispose()");
            if (_Connection != null)
            {
                _Connection.Close();
            }
        }


        public Microsoft.Data.SqlClient.SqlConnection CreateAndOpenConnection()
        {
            Console.WriteLine($"CreateAndOpenConnection() => {base.ToString()}");
            var connection = new Microsoft.Data.SqlClient.SqlConnection(base.ToString());
            connection.StateChange += OnStateChanged;
            connection.Open();
            return connection;
        }


        private void OnStateChanged(object sender, StateChangeEventArgs evt)
        {
            Console.WriteLine($"{evt.OriginalState}=>{evt.CurrentState}");
        }
    }
}
