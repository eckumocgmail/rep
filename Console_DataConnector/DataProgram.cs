using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using System.Reflection;
using System.Diagnostics;
using Console_DataConnector.DataModule.DataApi;
using Console_DataConnector.DataModule.DataADO.ADODbExecutorService;
using Console_DataConnector.DataModule.DataADO.ADODbMetadataServices;
using Console_DataConnector.DataModule.DataADO.ADODbMigBuilderService;
using Console_DataConnector.DataModule.DataADO;
using Console_DataConnector.DataModule.DataCommon.Metadata;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using System.Data;
using Google.Protobuf.WellKnownTypes;
using System.Drawing;

public class DataConnectionManager
{
    public Dictionary<string, string> Connections { get; private set; }

    public static void Start(ref string[] args)
    {
        var manager = new DataConnectionManager();
        string filePath = @"D:\System-Config\" + nameof(DataConnectionManager) + ".json";
        manager.GetDataSourceConnectionStringsFromFile( filePath);
    }

    public string GetByName(string name)
    {

        return Connections[name];
    }

    private void GetDataSourceConnectionStringsFromFile(string filePath)
    {
        if (System.IO.File.Exists(filePath) == false)
        {
            this.WriteDataSourceConnectionStringsToFile(filePath);
        }
        this.ReadDataSourceConnectionStringsFromFile(filePath);
    }

    private void ReadDataSourceConnectionStringsFromFile(string filePath)
    {
        this.Connections=System.IO.File.ReadAllText(filePath).FromJson<Dictionary<string, string>>();
    }

    private void WriteDataSourceConnectionStringsToFile(string filePath)
    {
        System.IO.File.WriteAllText(filePath, this.Connections.ToJsonOnScreen());
    }

    
}






namespace Console_DataConnector
{


    public class DataProgram : SuperType
    {
        public static string URL { get; set; } = System.IO.Directory.GetCurrentDirectory();
        public object ActionsMetaData { get; set; }


        public Dictionary<string, string> TypeMetaData { get; set; }
        private Dictionary<string, Dictionary<string, string>> PropertiesMetaData { get; set; }


        public override IDictionary<string, string> GetTypeAttributes()
        {
            throw new NotImplementedException();
        }

        public override IDictionary<string, string> GetPropertiesAttributes()
        {
            throw new NotImplementedException();
        }

        public override IDictionary<string, string> GetMethodAttributes()
        {
            throw new NotImplementedException();
        }




        static void Startup(string[] args)
        {
            Run(args);
            var odbc = new OdbcDriverManager();
            odbc.GetOdbcDrivers();
            using (var sql = new SqlServerADODataSource())
            {
                sql.EnsureIsValide();
            }

        }

        public static void CreateAction()
        {

            AppProviderService.GetInstance().Info($"Вызов: {Assembly.GetCallingAssembly().GetName().Name}");
            AppProviderService.GetInstance().Info($"Впроцессе: {Assembly.GetExecutingAssembly().GetName().Name}");

            foreach (var type in Assembly.GetExecutingAssembly().GetDataContexts())
            {
                var forProperties = Utils.ForAllPropertiesInType(type);
                var forMethods = Utils.ForAllMethodsInType(type);
                var forType = Utils.ForType(type);

                var superType = new DataProgram();
                superType.TypeMetaData = forType;
                superType.PropertiesMetaData = forProperties;
                superType.ActionsMetaData = forMethods;

                type.Info(superType.ToJsonOnScreen());
            }
        }

        public static void Run(string[] args)
        {
            SqlServerExecutor Executor = null;

            IdbMetadata DataSource = null;
            switch (ProgramDialog.SingleSelect("Какая база нужна?", new string[] {
                "SqlServer",
                "MySql",
                "Postgres",
                "Выход"

            }, ref args))
            {
                case "SqlServer":
                    var sqlserver = new SqlServerMigBuilder();
                    DataSource = sqlserver;
                    Executor = sqlserver;
                    
                    break;
                case "MySql":
                    var mysql = new MySqlDbMetadata();
                    DataSource = mysql;
                    //Executor = mysql;

                    break;
                case "Postgres":
                    var postgres = new PostgresDbMetadata();
                    DataSource = postgres;
                    //Executor = postgres;
                    break;
                case "Выход":
                    Process.GetCurrentProcess().Close();
                    break;
                default: throw new Exception("Нет обработки выбора");
            }



            var procedurecs = DataSource.GetProceduresMetadata("dbo");
            string procedure = ProgramDialog.SingleSelect("Выберите процедуру",
                procedurecs.Keys.ToList(), ref args);
            ProcedureMetadata procdureMetadata = procedurecs[procedure];
            AppProviderService.GetInstance().Info($"ExecuteProcedure({procdureMetadata.ProcedureName})");



            Console.Clear();

            InputConsole.Info("Введите параметры процедуры: ");
            using (var con = Executor.GetConnection())
            {
                SqlCommand command = new SqlCommand(procdureMetadata.FullName, con);
                command.CommandType = CommandType.StoredProcedure;
             
                var outparams = new List<SqlParameter>();
                var results = new Dictionary<string, string>();
                var arguments = new Dictionary<string, string>();
                                
                foreach (var kv in procdureMetadata.ParametersMetadata)
                {
                    ParameterMetadata parameterMetadata = kv.Value;
                        

                    SqlParameter parameter = new SqlParameter();
                    parameter.ParameterName = kv.Key;
                    switch (kv.Value.ParameterMode)
                    {
                        case "IN":      parameter.Direction = ParameterDirection.Input; break;
                        case "INOUT":   outparams.Add(parameter); parameter.Direction = ParameterDirection.InputOutput; break;
                        case "OUT":     outparams.Add(parameter); parameter.Direction = ParameterDirection.Output; break;
                        default: throw new Exception("На удалось определить модефикатор параметра ParameterDirection");

                    }
                        
                      
                    InputConsole.Get().Info(parameterMetadata.DataType.ToCapitalStyle());
                    switch (parameterMetadata.DataType.ToCapitalStyle())
                    {
                        //     System.Int64. A 64-bit signed integer.
                        case "Text":
                            {
                                parameter.SqlDbType = SqlDbType.Text;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputText($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "SmallMoney":
                            {
                                parameter.SqlDbType = SqlDbType.SmallMoney;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputSmallMoney($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "Structured":
                            {
                                parameter.SqlDbType = SqlDbType.Structured;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputStructured($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "Udt":
                            {
                                parameter.SqlDbType = SqlDbType.Udt;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputUdt($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "Date":
                            {
                                parameter.SqlDbType = SqlDbType.Date;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputDate($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "Time":
                            {
                                parameter.SqlDbType = SqlDbType.Time;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputTime($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "Variant":
                            {
                                parameter.SqlDbType = SqlDbType.Variant;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputVariant($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "Xml":
                            {
                                parameter.SqlDbType = SqlDbType.Xml;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputXml($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "VarBinary":
                            {
                                parameter.SqlDbType = SqlDbType.VarBinary;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputVarBinary($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "Timestamp":
                            {
                                parameter.SqlDbType = SqlDbType.Timestamp;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputTimestamp($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "TinyInt":
                            {
                                parameter.SqlDbType = SqlDbType.TinyInt;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputTinyInt($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "DateTimeOffset":
                            {
                                parameter.SqlDbType = SqlDbType.DateTimeOffset;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputDateTimeOffset($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "DateTime2":
                            {
                                parameter.SqlDbType = SqlDbType.DateTime2;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputDateTime2($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "SmallInt":
                            {
                                parameter.SqlDbType = SqlDbType.SmallInt;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputSmallInt($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "SmallDateTime":
                            {
                                parameter.SqlDbType = SqlDbType.SmallDateTime;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputSmallDateTime($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "UniqueIdentifier":
                            {
                                parameter.SqlDbType = SqlDbType.UniqueIdentifier;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputUniqueIdentifier($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "Real":
                            {
                                parameter.SqlDbType = SqlDbType.Real;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputReal($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "NVarChar":
                            {
                                parameter.SqlDbType = SqlDbType.NVarChar;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputNumber($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "NCHAR":
                            {
                                parameter.SqlDbType = SqlDbType.NChar;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputNchar($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "Money":
                            {
                                parameter.SqlDbType = SqlDbType.Money;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputMoney($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "Int":
                            {
                                parameter.SqlDbType = SqlDbType.Int;
                            parameter.Value = 0;
                                if (parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputInt($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "Image":
                            {
                                parameter.SqlDbType = SqlDbType.Image;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputImage($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "Float":
                            {
                                parameter.SqlDbType = SqlDbType.Float;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputFloat($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "Decimal":
                            {
                                parameter.SqlDbType = SqlDbType.Decimal;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputDecimal($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "DateTime":
                            {
                                parameter.SqlDbType = SqlDbType.DateTime;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputDateTime($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                            //
                        case "Char":
                            {
                                parameter.SqlDbType = SqlDbType.Char;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputChar($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        // 0 или 1
                        case "Bit": 
                            {
                                parameter.SqlDbType = SqlDbType.Bit;
                                if(parameter.Direction == ParameterDirection.Input )parameter.Value = InputConsole.InputBool($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
 
                        default: throw new NotSupportedException($"Тип данных хъ");
                    }
                    arguments[parameter.ParameterName] = parameter.Value.ToString();
                    command.Parameters.Add( parameter );
                }

                var reader = command.ExecuteReader();
                foreach(var column in reader.GetColumnSchema().Select(item => item.ColumnName))
                {
                    AppProviderService.GetInstance().Info(column+"\t");
                }
                while (reader.Read())
                {
                    foreach (var column in reader.GetColumnSchema().Select(item => item.ColumnName))
                    {
                        string s = reader[column].ToString();
                        while (s.Length < 20) s += " ";
                        Console.Write(s + "\t");
                    }
                    Console.WriteLine("");
                }

                outparams.ForEach(p => results[p.ParameterName] = p.Value.ToString());
                new
                {
                    Procedure = procdureMetadata.FullName,
                    In = arguments,
                    Out = results
                }.ToJsonOnScreen().WriteToConsole();

               
            }
                
            
        }

       
    }




}
