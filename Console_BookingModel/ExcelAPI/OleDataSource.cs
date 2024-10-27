
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class OleDataSource
    {
        private static string DEFAULT_PROVIDER = "Microsoft.ACE.OLEDB.12.0";

        private string provider;
        private string datasource;
        
        public OleDataSource(string provider, DataSourceEnumeration type, string datasource)
        {
            this.provider = provider;
            this.datasource = datasource;
        }

        public JArray GetTablesMetadata()
        {
            using (OleDbConnection connection = GetConnection())
            {
                DataTable dataTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                return this.Convert(dataTable);
            }
        }
        public OleDataSource(DataSourceEnumeration type, string datasource)
        {
            this.provider = DEFAULT_PROVIDER;
            switch (type)
            {
                case DataSourceEnumeration.Access:
                    this.datasource = datasource;
                    break;
                case DataSourceEnumeration.Excel:
                    this.datasource= string.Format("{0};Extended Properties=Excel 8.0;", datasource);
                   
                    break;
                default: throw new Exception("Unknown type of OleDb datasources");
            }
             
        }

        private string GetConnectionString()
        {
            return "Provider="+this.provider+";Data Source="+this.datasource;
        }

        private OleDbConnection GetConnection()
        {
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = GetConnectionString();
            connection.Open();
            return connection;
        }

        public string[] GetTables()
        {
            using (OleDbConnection connection = GetConnection())
            {                
                DataTable dataTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string[] tables = new string[dataTable.Rows.Count];     int i = 0;                  
                foreach (DataRow row in dataTable.Rows)
                {                                       
                    tables[i++] = row["TABLE_NAME"].ToString();
                }
                return tables;
            }
        }

        public Dictionary<string, object> GetDataModel()
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            using (OleDbConnection connection = GetConnection())
            {
                //model["catalogs"] = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Catalogs, null);
                Dictionary<string, object> tablesDictionary = new Dictionary<string, object>();
                DataTable tables = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                foreach(JObject table in this.Convert(tables))
                {
                    Console.WriteLine(table);
                    string tableName = table["tablE_NAME"].Value<string>();
                    string tableSchema = table["tablE_SCHEMA"].Value<string>();

                    Dictionary<string, object> columns = new Dictionary<string, object>();
                    foreach (JObject column in this.Convert( connection.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[]{ tableName })))
                    {
                        columns[columns.Count + ""] = column;
                    }

                    Dictionary<string, object> metadata = new Dictionary<string, object>();
                    metadata["name"] = tableName;
                    metadata["schema"] = tableSchema;
                    metadata["columns"] = columns;
                    tablesDictionary[tableName] = metadata;
                }
                model["tables"] = tables;

                
                model["views"] = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Views, null);
                model["foreign"] = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, null);
                model["primary"] = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Primary_Keys, null);
            }
            return model;
        }

        public Dictionary<string, object> GetMetadata()
        {
            Dictionary<string, object> metadata = new Dictionary<string, object>();
            using (OleDbConnection connection = GetConnection())
            {
                Guid[] guids = new Guid[]{
                    //
                    /// Сводка:
                    ///     Возвращает таблицы (включая представления), доступные для данного пользователя.
                    OleDbSchemaGuid.Tables_Info,
                    ///
                    /// Сводка:
                    ///     Возвращает статистические данные, определенные в каталоге, который принадлежит
                    ///     указанному пользователю.
                    OleDbSchemaGuid.Statistics,
                    ///
                    /// Сводка:
                    ///     Возвращает таблицы (включая представления) определены в каталоге, доступных для
                    ///     данного пользователя.
                    OleDbSchemaGuid.Tables,
                    ///
                    /// Сводка:
                    ///     Возвращает преобразования знаков, определенные в каталоге, который доступен указанному
                    ///     пользователю.
                    OleDbSchemaGuid.Translations,
                    ///
                    /// Сводка:
                    ///     Возвращает типы базовых данных, поддерживаемые поставщиком данных .NET Framework
                    ///     для OLE DB.
                    OleDbSchemaGuid.Provider_Types,
                    ///
                    /// Сводка:
                    ///     Возвращает представления, определенные в каталоге, который доступен указанному
                    ///     пользователю.
                    OleDbSchemaGuid.Views,
                    ///
                    /// Сводка:
                    ///     Возвращает столбцы, по которым просматриваемые таблицы зависят, определенные
                    ///     в каталоге и принадлежащие данному пользователю.
                    OleDbSchemaGuid.View_Column_Usage,
                    ///
                    /// Сводка:
                    ///     Возвращает таблиц, в котором просматриваемые таблицы, определенные в каталоге
                    ///     и принадлежащие данному пользователю зависят.
                    OleDbSchemaGuid.View_Table_Usage,
                    ///
                    /// Сводка:
                    ///     Возвращает сведения о параметрах и кодах возврата процедур.
                    OleDbSchemaGuid.Procedure_Parameters,
                    ///
                    /// Сводка:
                    ///     Возвращает внешние ключевые столбцы, определенные в каталоге данным пользователем.
                    OleDbSchemaGuid.Foreign_Keys,
                    ///
                    /// Сводка:
                    ///     Возвращает столбцы первичного ключа, определенные в каталоге данным пользователем.
                    OleDbSchemaGuid.Primary_Keys,
                    ///
                    /// Сводка:
                    ///     Возвращает сведения о столбцах наборов строк, возвращаемых процедурами.
                    OleDbSchemaGuid.Procedure_Columns,
                    ///
                    /// Сводка:
                    ///     Описывает доступный набор статистических данных по таблицам для поставщика.
                    OleDbSchemaGuid.Table_Statistics,
                    ///
                    /// Сводка:
                    ///     Возвращает ограничения проверки, определенные в каталоге, который принадлежит
                    ///     указанному пользователю.
                    OleDbSchemaGuid.Check_Constraints_By_Table,
                    ///
                    /// Сводка:
                    ///     Возвращает список строк схемы, идентифицируемых по их идентификаторам GUID и
                    ///     указатель на описания столбцов ограничений.
                    OleDbSchemaGuid.SchemaGuids,
                    ///
                    /// Сводка:
                    ///     Возвращает список ключевых слов конкретного поставщика.
                    OleDbSchemaGuid.DbInfoKeywords,
                    ///
                    /// Сводка:
                    ///     Возвращает уровни соответствия, параметры и диалекты, поддерживаемые данными
                    ///     обработки реализации SQL, определенные в каталоге.
                    OleDbSchemaGuid.Sql_Languages,
                    ///
                    /// Сводка:
                    ///     Возвращает объекты схемы, принадлежащие данному пользователю.
                    OleDbSchemaGuid.Schemata,
                    ///
                    /// Сводка:
                    ///     Возвращает процедуры, определенные в каталоге, который принадлежит указанному
                    ///     пользователю.
                    OleDbSchemaGuid.Procedures,
                    ///
                    /// Сводка:
                    ///     Возвращает привилегии USAGE для объектов, определенные в каталоге, которые доступны
                    ///     или предоставленных указанным пользователем.
                    OleDbSchemaGuid.Usage_Privileges,
                    ///
                    /// Сводка:
                    ///     Определяет доверенные объекты, заданные в источнике данных.
                    OleDbSchemaGuid.Trustee,
                    ///
                    /// Сводка:
                    ///     Возвращает утверждения, определенные в каталоге, который принадлежит указанному
                    ///     пользователю.
                    OleDbSchemaGuid.Assertions,
                    ///
                    /// Сводка:
                    ///     Возвращает физические атрибуты, связанные с каталогами, доступными из источника
                    ///     данных. Возвращает утверждения, определенные в каталоге, который принадлежит
                    ///     указанному пользователю.
                    OleDbSchemaGuid.Catalogs,
                    ///
                    /// Сводка:
                    ///     Возвращает наборы символов, определенные в каталоге, который доступен указанному
                    ///     пользователю.
                    OleDbSchemaGuid.Character_Sets,
                    ///
                    /// Сводка:
                    ///     Возвращает сравнения знаков, определенные в каталоге, который доступен указанному
                    ///     пользователю.
                    OleDbSchemaGuid.Collations,
                    ///
                    /// Сводка:
                    ///     Возвращает столбцы таблиц (включая представления), определенные в каталоге, который
                    ///     доступен указанному пользователю.
                    OleDbSchemaGuid.Columns,
                    ///
                    /// Сводка:
                    ///     Возвращает ограничения проверки, определенные в каталоге, который принадлежит
                    ///     указанному пользователю.
                    OleDbSchemaGuid.Check_Constraints,
                    ///
                    /// Сводка:
                    ///     Возвращает список литералов поставщика, используемых в текстовых командах.
                    OleDbSchemaGuid.DbInfoLiterals,
                    ///
                    /// Сводка:
                    ///     Возвращает столбцы, используемые ссылочные ограничения, ограничения unique, ограничения
                    ///     check и утверждения, определенные в каталоге и принадлежащие данному пользователю.
                    OleDbSchemaGuid.Constraint_Column_Usage,
                    ///
                    /// Сводка:
                    ///     Возвращает столбцы, определенные в каталоге, который ограничивается ключами данным
                    ///     пользователем.
                    OleDbSchemaGuid.Key_Column_Usage,
                    ///
                    /// Сводка:
                    ///     Возвращает ссылочные ограничения, определенные в каталоге, который принадлежит
                    ///     указанному пользователю.
                    OleDbSchemaGuid.Referential_Constraints,
                    ///
                    /// Сводка:
                    ///     Возвращает табличные ограничения, определенные в каталоге, который принадлежит
                    ///     указанному пользователю.
                    OleDbSchemaGuid.Table_Constraints,
                    ///
                    /// Сводка:
                    ///     Возвращает столбцы, определенные в каталоге, зависящие от домена, определенные
                    ///     в каталоге и принадлежащие данному пользователю.
                    OleDbSchemaGuid.Column_Domain_Usage,
                    ///
                    /// Сводка:
                    ///     Возвращает индексы, определенные в каталоге, который принадлежит указанному пользователю.
                    OleDbSchemaGuid.Indexes,
                    ///
                    /// Сводка:
                    ///     Возвращает привилегии для столбцов таблиц, определенные в каталоге, которые доступны
                    ///     или предоставленных указанным пользователем.
                    OleDbSchemaGuid.Column_Privileges,
                    ///
                    /// Сводка:
                    ///     Возвращает привилегии для таблиц, определенные в каталоге доступны или предоставленные
                    ///     указанному пользователю.
                    OleDbSchemaGuid.Table_Privileges,
                    ///
                    /// Сводка:
                    ///     Возвращает таблицы, используемые ссылочные ограничения, ограничения unique, ограничения
                    ///     check и утверждения, которые определены в каталоге и принадлежащие данному пользователю.
                    OleDbSchemaGuid.Constraint_Table_Usage

                };
                foreach(Guid guid in guids)
                {
                    try
                    {
                        Dictionary<string, object> resultSet = new Dictionary<string, object>();
                        DataTable dataTable = connection.GetOleDbSchemaTable(guid, null);
                        
                        List<Dictionary<string, object>> listRow = new List<Dictionary<string, object>>();
                        foreach (DataRow row in dataTable.Rows)
                        {
                            Dictionary<string, object> rowSet = new Dictionary<string, object>();
                            foreach (DataColumn column in dataTable.Columns)
                            {
                                rowSet[column.Caption] =row[column.Caption];
                            }
                            listRow.Add(rowSet);                             
                        }
                        resultSet["rows"]=listRow;
                        metadata[guid.ToString()] = resultSet;
                        
                    }catch(Exception ex)
                    {
                        Debug.WriteLine(ex);
                        continue;
                    }
                }                
                return metadata;
            }
        }

        public JArray Convert(DataTable table)
        {
            Dictionary<string, object> resultSet = new Dictionary<string, object>();
            List<Dictionary<string, object>> listRow = new List<Dictionary<string, object>>();
            foreach (DataRow row in table.Rows)
            {
                Dictionary<string, object> rowSet = new Dictionary<string, object>();
                foreach (DataColumn column in table.Columns)
                {
                    rowSet[column.Caption] = row[column.Caption];
                }
                listRow.Add(rowSet);
            }
            resultSet["rows"] = listRow;
            return (JArray)(JObject.FromObject(resultSet)["rows"]);
        }

        public JArray execute( string sql )
        {           
            using (OleDbConnection connection = GetConnection())
            {
                DataTable dataTable = new DataTable();
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection);
                adapter.Fill(dataTable);

                Dictionary<string, object> resultSet = new Dictionary<string, object>();
                List<Dictionary<string, object>> listRow = new List<Dictionary<string, object>>();
                foreach (DataRow row in dataTable.Rows)
                {
                    Dictionary<string, object> rowSet = new Dictionary<string, object>();
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        rowSet[column.Caption] = row[column.Caption];
                    }
                    listRow.Add(rowSet);
                }
                resultSet["rows"] = listRow;

                /*
                Dictionary<string, object> metadata = new Dictionary<string, object>();
                foreach (DataColumn column in dataTable.Columns)
                {
                    metadata[column.Caption] = JObject.FromObject(column);
                }
                resultSet["metadata"] = metadata;
                
                return metadata;*/


                JObject jrs = JObject.FromObject(resultSet);

                return (JArray)jrs["rows"]; 
            }
            
        }
    }
