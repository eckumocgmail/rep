using Console_DataConnector.DataModule.DataADO.ADODbExecutorService;
using Console_DataConnector.DataModule.DataCommon.Metadata;
using DataCommon;
using DataCommon.DatabaseMetadata;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataADO.ADODbMetadataServices
{
    /*
    public string DropAndCreateTable(string TableName) 
    { 
        return $@"
            DECLARE @COUNT INT
            SET @COUNT=(SELECT Count(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='{TableName}')
            IF( @COUNT > 0 )
            BEGIN
                DROP TABLE {TableName}
            END
            CREATE TABLE History( 
                ID INT PRIMARY KEY IDENTITY, 
                COMMAND nvarchar(512) NOT NULL,
                CREATED DateTime NOT NULL DEFAULT(GetDate())                 
            )
        ";
    }
     */
    /// <summary>
    /// 
    /// </summary>
    public class SqlServerDbMetadata : SqlServerExecutor, IDbMetadata
    {

        private IDictionary<string, TableMetadata> TablesMetadata { get; set; } = null;

        /// <summary>
        /// Внешние ключи
        /// Ключ = [TableName].[ColumnName] 
        /// Значение = [TableName].[ColumnName] 
        /// </summary>
        private IDictionary<string, string> ForeignKeys { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Параметры хранимых процедур с доступом по схемам
        /// </summary>
        private IDictionary<string, IDictionary<string, ProcedureMetadata>> ProceduresMetadata { get; set; } =
            new Dictionary<string, IDictionary<string, ProcedureMetadata>>();


        public SqlServerDbMetadata(): base()
        {
        }

        public SqlServerDbMetadata(string server, string database) : base(server, database)
        {
        }

        public SqlServerDbMetadata(string server, string database, bool trustedConnection, string userID, string password) : base(server, database, trustedConnection, userID, password)
        {
        }


        /// <summary>
        /// Получение наименований таблиц
        /// </summary>
        /// <returns>Наименования таблиц</returns>
        public IEnumerable<string> GetTableNames()
        {
            Console.WriteLine($"GetTableNames()");
            DataTable ResultSet = ExecuteQuery(@"SELECT * FROM INFORMATION_SCHEMA.TABLES");
            return DataTableService.GetTextColumn(ResultSet, "TABLE_NAME");
        }


        /// <summary>
        /// Получение сведений о таблицах базы данных
        /// </summary>
        /// <returns>сведения о таблицах базы данных</returns>
        public IDictionary<string, TableMetadata> GetTablesMetadata()
        {
            Console.WriteLine($"GetTablesMetadata()");
            lock (this)
            {
                if (TablesMetadata == null)
                {
                    TablesMetadata = InitTablesMetadata();
                }
            }
            return TablesMetadata;
        }


        /// <summary>
        /// Получение сведений о таблицах базы данных
        /// </summary>
        /// <returns>сведения о таблицах базы данных</returns>
        private IDictionary<string, TableMetadata> InitTablesMetadata()
        {
            Console.WriteLine($"InitTablesMetadata()");
            IDictionary<string, TableMetadata> result = new Dictionary<string, TableMetadata>();
            var TablesInfoDT = ExecuteQuery(@"SELECT * FROM INFORMATION_SCHEMA.TABLES");
            IEnumerable<TableMetadata> TableMetadataArr = DataTableService.GetResultSet<TableMetadata>(TablesInfoDT);
            foreach (var next in TableMetadataArr)
            {
                next.ToJsonOnScreen().WriteToConsole();
                result[next.TableName] = next;
                next.ColumnsMetadata = GetColumnsMetadata(next.TableSchema, next.TableName);
            }
            IEnumerable<KeyMetadata> KeysMetadata = GetKeysMetadata();
            foreach (KeyMetadata Key in KeysMetadata)
            {
                ForeignKeys[Key.SourceTable + "." + Key.SourceColumn] = Key.TargetTable + "." + Key.TargetColumn;
                if (Key.TargetTable != null)
                {
                    result[Key.SourceTable].ForeignKeys[Key.SourceColumn] = Key.TargetTable;
                }
                else
                {
                    result[Key.SourceTable].PrimaryKey = Key.SourceColumn;
                }
            }
            return result;
        }


        /// <summary>
        /// Получение сведений о структуре данных таблицы
        /// </summary>
        /// <param name="TableSchema">Имя схемы</param>
        /// <param name="TableName">Имя таблицы</param>
        /// <returns>Сведения о структуре данных таблицы</returns>
        public IDictionary<string, ColumnMetadata> GetColumnsMetadata(string TableSchema, string TableName)
        {
            Console.WriteLine($"GetColumnsMetadata({TableSchema},{TableName})");
            IDictionary<string, ColumnMetadata> Result = new Dictionary<string, ColumnMetadata>();
            DataTable ColumnsDataTable = ExecuteQuery($@"SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='{TableSchema}' AND TABLE_NAME='{TableName}'");
            IEnumerable<ColumnMetadata> ColumnsMetaData = DataTableService.GetResultSet<ColumnMetadata>(ColumnsDataTable);
            foreach (var Column in ColumnsMetaData)
            {
                Result[Column.ColumnName] = Column;
            }
            return Result;
        }


        /// <summary>
        /// Получение сведений о внешних ключах
        /// </summary>
        /// <returns>сведения о внешних ключах</returns>
        public IEnumerable<KeyMetadata> GetKeysMetadata()
        {
            Console.WriteLine($"GetKeysMetadata()");
            DataTable KeysDataTable =
                ExecuteQuery("\n SELECT " +
                                " CCU.TABLE_NAME AS SOURCE_TABLE " +
                                ",CCU.COLUMN_NAME AS SOURCE_COLUMN " +
                                ",KCU.TABLE_NAME AS TARGET_TABLE " +
                                ",KCU.COLUMN_NAME AS TARGET_COLUMN " +
                            "FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE CCU " +
                                "INNER JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RC " +
                                " ON CCU.CONSTRAINT_NAME = RC.CONSTRAINT_NAME " +
                                "INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU " +
                                " ON KCU.CONSTRAINT_NAME = RC.UNIQUE_CONSTRAINT_NAME " +
                            "ORDER BY CCU.TABLE_NAME\n");
            return DataTableService.GetResultSet<KeyMetadata>(KeysDataTable);
        }





        /// <summary>
        /// Получение сведений о хранимых процедурах
        /// </summary>
        /// <param name="Schema">Имя схемы</param>
        /// <returns>сведения о хранимых процедурах</returns>
        public IDictionary<string, ProcedureMetadata> GetProceduresMetadata(string Schema="dbo")
        {
            Console.WriteLine($"GetProceduresMetadata({Schema})");
            lock (this)
            {
                if (ProceduresMetadata.ContainsKey(Schema) == false)
                {
                    ProceduresMetadata[Schema] = InitProceduresMetadata(Schema);
                }
            }
            return ProceduresMetadata[Schema];
        }

        /// <summary>
        /// Получение сведений о хранимых процедурах
        /// </summary>
        /// <param name="Schema">Имя схемы</param>
        /// <returns>сведения о хранимых процедурах</returns>
        private IDictionary<string, ProcedureMetadata> InitProceduresMetadata(string Schema)
        {

            Console.WriteLine($"InitProceduresMetadata({Schema})");
            IDictionary<string, ProcedureMetadata> ProceduresMetadataDictionary = new Dictionary<string, ProcedureMetadata>();
            DataTable SPDataTable = ExecuteQuery(@"EXEC sp_stored_procedures");
            int proceduresCount = SPDataTable.Rows.Count;
            int n = 1;
            foreach (DataRow row in SPDataTable.Rows)
            {
                this.Info($"Процедура №{n++} (Всего {proceduresCount})");
                string ProcedureName = row["PROCEDURE_NAME"].ToString();
                ProcedureName = ProcedureName.Substring(0, ProcedureName.LastIndexOf(";"));
                string ProcedureOwner = row["PROCEDURE_OWNER"].ToString();
                string ProcedureQualifier = row["PROCEDURE_QUALIFIER"].ToString();

                ProceduresMetadataDictionary[ProcedureName] = new ProcedureMetadata();                
                ProcedureMetadata ProcedureMetadata = GetProcedureMetadata(ProcedureOwner, ProcedureName);
                ProcedureMetadata.ProcedureQualifier = ProcedureQualifier;

                ProcedureMetadata.ParametersMetadata = GetParametersMetadata(ProcedureOwner, ProcedureName);
                ProcedureMetadata.EnsureIsValide();
                ProceduresMetadataDictionary[ProcedureName] = ProcedureMetadata;
                if (n++ == 10)
                    break;
            }
            return ProceduresMetadataDictionary;
        }


        /// <summary>
        /// Получение сведений о хранимой процедуре
        /// </summary>
        /// <param name="SchemaName">Имя схемы</param>
        /// <param name="ProcedureName">Имя процедуры</param>
        /// <returns>Сведения о хранимой процедуре</returns>
        public ProcedureMetadata GetProcedureMetadata(string SchemaName, string ProcedureName)
        {
            Console.WriteLine($"GetProcedureMetadata({SchemaName},{ProcedureName})");
            ProcedureMetadata ProcedureMetadata = new ProcedureMetadata()
            {
                ProcedureName = ProcedureName,
                ProcedureOwner = SchemaName
            };
            ProcedureMetadata.ParametersMetadata = GetParametersMetadata(SchemaName, ProcedureName);            
            return ProcedureMetadata;
        }


        /// <summary>
        /// Получение сведений о параметрах хранимой процедуры
        /// </summary>
        /// <param name="SchemaName">Имя схемы</param>
        /// <param name="ProcedureName">Имя процедуры</param>
        /// <returns>Информация о параметрах</returns>
        public IDictionary<string, ParameterMetadata> GetParametersMetadata(string SchemaName, string ProcedureName)
        {
            Console.WriteLine($"GetParameterMetadata({SchemaName},{ProcedureName})");
            IDictionary<string, ParameterMetadata> result = new Dictionary<string, ParameterMetadata>();
            var TablesInfoDT = ExecuteQuery($@"SELECT * FROM INFORMATION_SCHEMA.PARAMETERS WHERE SPECIFIC_SCHEMA='{SchemaName}' AND SPECIFIC_NAME='{ProcedureName}'");
            IEnumerable<ParameterMetadata> ParameterMetadataArr = DataTableService.GetResultSet<ParameterMetadata>(TablesInfoDT);
            List<ParameterMetadata> ParameterMetadataList = new List<ParameterMetadata>(ParameterMetadataArr);
            ParameterMetadataList.Sort((pm1, pm2) => pm2.OrdinalPosition - pm1.OrdinalPosition);
            foreach (var next in ParameterMetadataList)
            {
                result[next.ParameterName] = next;
            }
            return result;
        }


    }
}
