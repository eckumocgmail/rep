using Newtonsoft.Json.Linq;

using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;

using System.Threading.Tasks;
using Console_DataConnector.DataModule.DataApi;
using Console_DataConnector.DataModule.DataCommon.DatabaseMetadata;
using Console_DataConnector.DataModule.DataCommon.DataTypes;
using Console_DataConnector.DataModule.DataODBC.Manager;
using Console_DataConnector.DataModule.DataODBC.Connectors;

namespace Console_DataConnector.DataModule.DataCommon.Services { }
/// <summary>
/// 
/// </summary>
public class OdbcDatabaseManager : MyValidatableObject
{
    public static ConcurrentDictionary<string, OdbcDatabaseManager> DATASOURCES = new ConcurrentDictionary<string, OdbcDatabaseManager>();
    public static ConcurrentDictionary<string, Dictionary<string, int>> STATISTICS = new ConcurrentDictionary<string, Dictionary<string, int>>();
    public static ConcurrentDictionary<string, DatabaseMetadata> METADATA = new ConcurrentDictionary<string, DatabaseMetadata>();
    public static ConcurrentDictionary<string, int> KEYWORDS = new ConcurrentDictionary<string, int>();
    public string[] Actions() => new string[] { "$stats", "$keywords", "$errors", "$rating", "$latest", "$popular", "$metadata", "$list" };

    [NotInput]
    public Action<object> OnReady { get; set; } = (evt) => { };
    [NotInput]
    public bool IsReady { get; set; }


    //private static string SQL_SERVERL_ODBC_DRIVER = "Driver={SQL Server};";

    public Dictionary<string, OdbcTableManager> fasade = new Dictionary<string, OdbcTableManager>();
    public Dictionary<string, TableManagerStatefull> memory = new Dictionary<string, TableManagerStatefull>();


    public OdbcDatabaseManager(APIDataSource odbc)
    {
        Init(ds = odbc);
    }



    public OdbcTableManager GetFasade<T>()
    {
        return fasade[typeof(T).Name];
    }

    private object GetErrors() => throw new NotImplementedException();
    private object GetStats() => throw new NotImplementedException();
    private object GetRating() => throw new NotImplementedException();
    private object GetLatest() => throw new NotImplementedException();
    private object GetPopular() => throw new NotImplementedException();
    private object GetCommands() => throw new NotImplementedException();




    public static OdbcDatabaseManager GetOdbc(string name)
    {

        string dns = name;
        string login = "root";
        string password = "sgdf1423";

        return GetOdbcDatabaseManager("dsn=" + dns + ";UID=" + login + ";PWD=" + password + ";");
    }
    public static OdbcDatabaseManager GetOdbcDatabaseManager()
    {
        string dns = "ASpbMarketPlace";
        string login = "root";
        string password = "sgdf1423";

        return GetOdbcDatabaseManager("dsn=" + dns + ";UID=" + login + ";PWD=" + password + ";");
    }

    public OdbcTableManager GetTableManager(string tableName)
    {
        if (fasade.ContainsKey(tableName))
        {
            return fasade[tableName];
        }
        else
        {
            var dbm = GetMetaData();
            if (new List<string>(dbm.Tables.Keys).Contains(tableName) == false)
            {
                throw new Exception("Не найдена таблица базы данных с именем " + tableName);
            }
            else
            {
                return fasade[tableName] = new OdbcTableManager(tableName, GetDataSource(), dbm.Tables[tableName]);
            }
        }
    }


    public static OdbcDatabaseManager GetOdbcDatabaseManager(string odbcConnectionString)
    {

        if (DATASOURCES.ContainsKey(odbcConnectionString) == false)
        {
            APIDataSource ds = new OdbcDataSource(odbcConnectionString);
            OdbcDatabaseManager dbm = new OdbcDatabaseManager(ds);
            DATASOURCES[odbcConnectionString] = dbm;
            dbm.discovery();
        }
        return DATASOURCES[odbcConnectionString];
    }



    public List<string> GetTables()
    {
        return ds.GetTables().ToList();
    }

    /*public void ImportFromCsvFile(string tableName, string fileName)
    {
        List<Dictionary<string, object>> data =
            CsvReader.Parse(fileName, fasade[tableName].GetMetadata());
        int n = 1;
        foreach (var record in data)
        {
            fasade[tableName].Create(record);

            long ctn = fasade[tableName].Count();
            if (ctn != n)
            {
                throw new Exception("Строка " + n + " не сохранилась");
            }
            else
            {
                n++;
            }
        }
    }*/

    /*
    public static void createSqlServerAdoDataSource(string name, string connectionString)
    {
        string odbcConnectionString = SQL_SERVERL_ODBC_DRIVER + connectionString.Replace(@"\\", @"\");
        if (DATASOURCES.ContainsKey(odbcConnectionString) == false)
        {
            APIDataSource ds = new SqlServerAPIDataSource(odbcConnectionString);
            DatabaseManager dbm = new DatabaseManager(ds);
            DATASOURCES[name] = dbm;
            //dbm.discovery();
        }
    }



         
    */

    public void Use(APIDataSource ds)
    {
        fasade = new Dictionary<string, OdbcTableManager>();
        Init(this.ds = ds);
    }





    public Dictionary<string, int> GetKeywords()
    {
        if (keywords == null || keywords.Count == 0)

            discovery();

        return keywords;
    }


    public void discovery()
    {

        Dictionary<string, int> keywords = new Dictionary<string, int>(KEYWORDS);
        Dictionary<string, object> statistics = new Dictionary<string, object>();
        if (KEYWORDS.Count > 0 && STATISTICS.Count > 0)
        {
            this.keywords = new Dictionary<string, int>(KEYWORDS);
            this.statistics = new Dictionary<string, object>();
            return;
        }
        try
        {
            foreach (var pair in fasade)
            {
                string name = pair.Key;
                TableMetaData metadata = pair.Value.GetMetadata();
                string pk = GetMetaData().Tables.ContainsKey(metadata.singlecount_name) ?
                            GetMetaData().Tables[metadata.singlecount_name].getPrimaryKey() :
                            GetMetaData().Tables[metadata.name].getPrimaryKey();
                if (pk == null)
                {
                    throw new Exception("Primary key udefined for table " + name);
                }
                var tms = new TableManagerStatefull(pair.Value);
                List<string> textColumns = pair.Value.GetMetadata().GetTextColumns();
                foreach (JObject record in tms.dataRecords)
                {
                    try
                    {
                        if (true) Console.WriteLine(record);
                        int id = pk is not null ? record[record.ContainsKey(pk)? pk: record.ContainsKey("Id")? "Id": "id"].Value<int>(): -1;

                        Dictionary<string, int> statisticsForThisRecord = new Dictionary<string, int>();
                        foreach (string column in textColumns)
                        {
                            if (record[column] != null)
                            {
                                if (true) Console.WriteLine(record.ToString());
                                string textValue = record[column].Value<string>();
                                if (string.IsNullOrEmpty(textValue)) continue;
                                foreach (string word in textValue.Split(" "))
                                {
                                    bool isRus = word.IsRus();
                                    bool isEng = word.IsEng();
                                    if (isRus == false && isEng == false)
                                        continue;
                                    if (keywords.ContainsKey(word))
                                    {
                                        keywords[word]++;
                                    }
                                    else
                                    {
                                        keywords[word] = 1;
                                    }

                                    if (statisticsForThisRecord.ContainsKey(word))
                                    {
                                        statisticsForThisRecord[word]++;
                                    }
                                    else
                                    {
                                        statisticsForThisRecord[word] = 1;
                                    }
                                }
                            }
                        }

                        statistics[name + "/" + id] = statisticsForThisRecord;
                    }
                    catch (Exception ex)
                    {
                        if (true) Console.WriteLine(ex);
                        continue;
                    }


                }

            }
        }
        catch (Exception ex)
        {
            keywords[ex.Message] = 500;
            if (true) Console.WriteLine(ex);
        }
        this.keywords = keywords;
        this.statistics = statistics;
    }




    /// <summary>
    /// Метод получения обьектов управления таблицами базы данных
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, OdbcTableManager> GetFasade()
    {
        return fasade;
    }


    /// <summary>
    /// Метод получения обьекта управления таблицей базы данных
    /// </summary>
    /// <param name="name"> имя таблицы или сущности</param>
    /// <returns></returns>
    public OdbcTableManager Get(string name)
    {
        if (fasade.ContainsKey(name))
        {

            return fasade[name];
        }
        else
        {
            throw new Exception($"Имя таблицы: {name} задано неверно");
        }
    }

    public object GetAssociations(string key)
    {
        switch (key)
        {
            case "$list": return GetCommands();
            case "$metadata": return GetMetadata();
            case "$popular": return GetPopular();
            case "$latest": return GetLatest();
            case "$rating": return GetRating();
            case "$stats": return GetStats();
            case "$keywords": return GetKeywords();
            case "$errors": return GetErrors();
            default: return null;
        }
    }



    public void DumpDatabase()
    {

    }

    public DatabaseMetadata GetDatabaseMetadata()
    {
        return ds.GetDatabaseMetadata();
    }

    public DatabaseMetadata GetMetadata()
    {
        if (METADATA.ContainsKey(ds.GetConenctionString()) == false)
        {
            METADATA[ds.GetConenctionString()] = ds.GetDatabaseMetadata();
        }
        return METADATA[ds.GetConenctionString()];
    }


    /*private DatabaseSnapshot CreateDump()
    {
        DatabaseSnapshot dump = new DatabaseSnapshot();
        DatabaseMetadata dbm = GetDataSource().GetDatabaseMetadata();
        dump.metadata = dbm;
        foreach (string tm in dbm.Tables.Keys)
        {
            dump.datasets[tm] = GetDataSource().Execute("select * from " + tm);
        }
        return dump;
    }*/
    /*
    public DataModel( ) : base()
    {
        Database.EnsureCreated();
    }

    public DataModel( DbContextOptions<DataModel> options ) : base( options )
    {
        Database.EnsureCreated();
    }


    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
    {"jdbc:postgresql://localhost:4200/postgres", "mister_kest", "Kest1423"
        //optionsBuilder.UseSqlServer(@"Server=511-5A;Database=webapp;Trusted_Connection=True;");
        //optionsBuilder.UseInMemoryDatabase();// UseMySql("server=localhost;UserId=root;Password=password;database=usersdb3;");
        //optionsBuilder.UseMySQL( "server=localhost;database=library;user=root;password=root" );
    }*/

    //new JdbcConnector("jdbc:postgresql://localhost:4200/postgres","mister_kest", "Kest1423");

    private APIDataSource ds;
    /*string datasource; 
    string username; 
    string password;*/
    public Dictionary<string, object> statistics = new Dictionary<string, object>();

    Dictionary<string, int> keywords = new Dictionary<string, int>();

    public APIDataSource GetDataSource()
    {
        this.EnsureIsValide();
        return ds;
    }
    /*
    public DatabaseManager(string datasource, string username, string password) : base()
    {
        this.datasource = datasource;
        this.username = username;
        this.password = password;
        this.ds = new APIDataSource(datasource, username, password);
        foreach (var prop in GetMetaData().Tables)
        {
            OdbcTableManager manager = new OdbcTableManager(prop.Key, GetDataSource(), prop.Value);
            fasade[prop.Key] = manager;//new TableManager(this, manager);
        }

    }*/



    public void Init(APIDataSource ds)
    {
        if (true) Console.WriteLine("Инициаллизация источника данных ODBC: ");
        if (true) Console.WriteLine("\t строка соединения: " + (this.ds = ds).GetConenctionString());
        fasade = new Dictionary<string, OdbcTableManager>();
        var dbm = GetMetaData();
        foreach (var prop in dbm.Tables)
        {
            Console.WriteLine(prop.Key);
            OdbcTableManager manager = new OdbcTableManager(prop.Key, GetDataSource(), prop.Value);
            TableManagerStatefull statefull = new TableManagerStatefull(this, manager);
            fasade[prop.Key] = manager;
            memory[prop.Key] = statefull;
            //fasade[prop.Value.singlecount_name.ToUpper()] = statefull;
            //fasade[prop.Value.multicount_name.ToUpper()] = statefull;
        }
    }

    public DatabaseMetadata GetMetaData()
    {
        return GetDataSource().GetDatabaseMetadata();
    }

    public Dictionary<string, object> ValidateDatabaseMetadata(Dictionary<string, object> tables)
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        foreach (var p in tables)
        {
            //TODO:
        }
        return result;
    }

    public JArray Execute(string sql)
    {
        return GetDataSource().GetJsonResult(sql);
    }

    public IEnumerable<string> GetForeignKeys(string table)
    {
        var keys = GetDatabaseMetadata().Keys;

        return keys.Where(key => key.Key.StartsWith(table + ".")).Select(kv => $"{kv.Key}:{kv.Value}");


    }

    /*
        public DataTable ExecuteToDataTable(string sql)
        {
            return GetDataSource().ExecuteDT(sql);
        }
    */



}
