using Console_DataConnector.DataModule.DataADO.ADODbExecutorService;
using Console_DataConnector.DataModule.DataCommon.Metadata;
using DataCommon.DatabaseMetadata;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataADO.ADODbMetadataServices
{
    public class PostgresDbMetadata : PostgresExecutor, IdbMetadata
    {
        public PostgresDbMetadata()
        {
        }

        public PostgresDbMetadata(string dataSource, int port, string database, string userId, string password) : base(dataSource, port, database, userId, password)
        {
        }

        public IEnumerable<string> GetTableNames()
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, TableMetadata> GetTablesMetadata()
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, ColumnMetadata> GetColumnsMetadata(string TableSchema, string TableName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<KeyMetadata> GetKeysMetadata()
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, ProcedureMetadata> GetProceduresMetadata(string Schema)
        {
            throw new NotImplementedException();
        }

        public ProcedureMetadata GetProcedureMetadata(string SchemaName, string ProcedureName)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, ParameterMetadata> GetParametersMetadata(string SchemaName, string ProcedureName)
        {
            throw new NotImplementedException();
        }
    }
}
