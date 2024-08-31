using Console_DataConnector.DataModule.DataADO.ADODbConnectionStrings;
using Npgsql;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataADO.ADODbConnectorService
{
    public class PostgresDbConnector : PostgresConnectionString,
        IdbConnector<NpgsqlConnection>,
        IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        private NpgsqlConnection _Connection;






        public PostgresDbConnector()
        {
        }

        public PostgresDbConnector(string dataSource, int port, string database, string userId, string password) : base(dataSource, port, database, userId, password)
        {
        }


        public NpgsqlConnection GetConnection()
        {
            if (_Connection == null)
            {
                _Connection = CreateAndOpenConnection();
            }
            return _Connection;
        }



        public new void Dispose()
        {
            if (_Connection != null)
            {
                _Connection.Close();
            }
        }



        public NpgsqlConnection CreateAndOpenConnection()
        {
            var connection = new NpgsqlConnection(base.ToString());

            connection.StateChange += OnStateChanged;
            connection.Open();
            return connection;
        }

        private void OnStateChanged(object sender, StateChangeEventArgs evt)
        {
            this.Info($"{evt.OriginalState}=>{evt.CurrentState}");
        }

    }
}
