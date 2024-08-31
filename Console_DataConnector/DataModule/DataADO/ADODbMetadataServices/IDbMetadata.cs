using Console_DataConnector.DataModule.DataADO.ADODbModelService;
using Console_DataConnector.DataModule.DataCommon.Metadata;
using DataCommon.DatabaseMetadata;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataADO.ADODbMetadataServices
{
    public interface IdbMetadata
    {

        public IEnumerable<string> GetTableNames();
        public IDictionary<string, TableMetadata> GetTablesMetadata();
        public IDictionary<string, ColumnMetadata> GetColumnsMetadata(string TableSchema, string TableName);
        public IEnumerable<KeyMetadata> GetKeysMetadata();
        public IDictionary<string, ProcedureMetadata> GetProceduresMetadata(string Schema);
        public ProcedureMetadata GetProcedureMetadata(string SchemaName, string ProcedureName);
        public IDictionary<string, ParameterMetadata> GetParametersMetadata(string SchemaName, string ProcedureName);
    }

    public class DbMetadata : IdbMetadata
    {
        public IDictionary<string, TableMetadata> Tables { get; set; } = new Dictionary<string, TableMetadata>();
        public IDictionary<string, ColumnMetadata> Columns { get; set; } = new Dictionary<string, ColumnMetadata>();
        public IEnumerable<KeyMetadata> Keys { get; set; } = new List<KeyMetadata>();
        public IDictionary<string, ProcedureMetadata> Procedures { get; set; } = new Dictionary<string, ProcedureMetadata>();
        public IDictionary<string, ParameterMetadata> Parameters { get; set; } = new Dictionary<string, ParameterMetadata>();

        public IEnumerable<string> GetTableNames() => Tables.Keys;
        public IDictionary<string, TableMetadata> GetTablesMetadata() => Tables;

        public IDictionary<string, ColumnMetadata> GetColumnsMetadata(string TableSchema, string TableName) => Columns;

        public IEnumerable<KeyMetadata> GetKeysMetadata() => Keys;
        public IDictionary<string, ProcedureMetadata> GetProceduresMetadata(string Schema) => Procedures;
        public ProcedureMetadata GetProcedureMetadata(string SchemaName, string ProcedureName) => Procedures[ProcedureName];

        public IDictionary<string, ParameterMetadata> GetParametersMetadata(string SchemaName, string ProcedureName) => Parameters;
    }
}

