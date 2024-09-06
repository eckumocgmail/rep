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
 
namespace Console_DataConnector
{


    public class DataProgram : SuperType
    {
        public static string URL { get; set; } = System.IO.Directory.GetCurrentDirectory();
        public object ActionsMetaData { get; set; }


        public Dictionary<string, string> TypeMetaData { get; set; }
        private Dictionary<string, Dictionary<string, string>> PropertiesMetaData { get; set; }

         




        static void Startup(string[] args)
        {
            Run(ref args);
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

        public static void Run(ref string[] args)
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
                                
                foreach (KeyValuePair<string,ParameterMetadata> kv in procdureMetadata.ParametersMetadata)
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

                    InputConsole.Clear();
                    InputConsole.Get().Info(kv.Key + " " + parameterMetadata );
                    if (parameterMetadata.DataType is null)
                        throw new Exception("DataType не задан");
                    switch (parameterMetadata.DataType.ToCapitalStyle().ToLower())
                    {
                        case "text":
                            {
                                parameter.SqlDbType = SqlDbType.Text;
                                if (parameter.Direction == ParameterDirection.Input)
                                {
                                    var test = InputConsole.InputText("test", val => null, ref args);
                                    parameter.Value = InputConsole.InputText(
                                        $"{kv.Key} {kv.Value.DataType}",
                                        (val) => null,
                                        ref args);
                                }
                                break;
                            }
                        case "smallmoney":
                            {
                                parameter.SqlDbType = SqlDbType.SmallMoney;
                                if(parameter.Direction == ParameterDirection.Input  || parameter.Direction == ParameterDirection.InputOutput )parameter.Value = InputConsole.InputSmallMoney($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "structured":
                            {
                                parameter.SqlDbType = SqlDbType.Structured;
                                if(parameter.Direction == ParameterDirection.Input  || parameter.Direction == ParameterDirection.InputOutput )parameter.Value = InputConsole.InputStructured($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "udt":
                            {
                                parameter.SqlDbType = SqlDbType.Udt;
                                if(parameter.Direction == ParameterDirection.Input  || parameter.Direction == ParameterDirection.InputOutput )parameter.Value = InputConsole.InputUdt($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "date":
                            {
                                parameter.SqlDbType = SqlDbType.Date;
                                if(parameter.Direction == ParameterDirection.Input  || parameter.Direction == ParameterDirection.InputOutput )parameter.Value = InputConsole.InputDate($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "time":
                            {
                                parameter.SqlDbType = SqlDbType.Time;
                                if(parameter.Direction == ParameterDirection.Input  || parameter.Direction == ParameterDirection.InputOutput )parameter.Value = InputConsole.InputTime($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "variant":
                            {
                                parameter.SqlDbType = SqlDbType.Variant;
                                if(parameter.Direction == ParameterDirection.Input  || parameter.Direction == ParameterDirection.InputOutput )parameter.Value = InputConsole.InputVariant($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "xml":
                            {
                                parameter.SqlDbType = SqlDbType.Xml;
                                if(parameter.Direction == ParameterDirection.Input  || parameter.Direction == ParameterDirection.InputOutput )parameter.Value = InputConsole.InputXml($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "varbinary":
                            {
                                parameter.SqlDbType = SqlDbType.VarBinary;
                                if(parameter.Direction == ParameterDirection.Input  || parameter.Direction == ParameterDirection.InputOutput )parameter.Value = InputConsole.InputVarBinary($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "timestamp":
                            {
                                parameter.SqlDbType = SqlDbType.Timestamp;
                                if(parameter.Direction == ParameterDirection.Input  || parameter.Direction == ParameterDirection.InputOutput )parameter.Value = InputConsole.InputTimestamp($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "tinyInt":
                            {
                                parameter.SqlDbType = SqlDbType.TinyInt;
                                if(parameter.Direction == ParameterDirection.Input  || parameter.Direction == ParameterDirection.InputOutput )parameter.Value = InputConsole.InputTinyInt($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "datetimeoffset":
                            {
                                parameter.SqlDbType = SqlDbType.DateTimeOffset;
                                if(parameter.Direction == ParameterDirection.Input  || parameter.Direction == ParameterDirection.InputOutput )
                                    parameter.Value = InputConsole.InputDateTimeOffset($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "datetime2":
                            {
                                parameter.SqlDbType = SqlDbType.DateTime2;
                                if(parameter.Direction == ParameterDirection.Input  || parameter.Direction == ParameterDirection.InputOutput )parameter.Value = InputConsole.InputDateTime2($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "smallint":
                            {
                                parameter.SqlDbType = SqlDbType.SmallInt;
                                if(parameter.Direction == ParameterDirection.Input  || parameter.Direction == ParameterDirection.InputOutput )parameter.Value = InputConsole.InputSmallInt($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "smalldatetime":
                            {
                                parameter.SqlDbType = SqlDbType.SmallDateTime;
                                if(parameter.Direction == ParameterDirection.Input  || parameter.Direction == ParameterDirection.InputOutput )parameter.Value = InputConsole.InputSmallDateTime($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "uniqueidentifier":
                            {
                                parameter.SqlDbType = SqlDbType.UniqueIdentifier;
                                if(parameter.Direction == ParameterDirection.Input  || parameter.Direction == ParameterDirection.InputOutput )parameter.Value = InputConsole.InputUniqueIdentifier($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "real":
                            {
                                parameter.SqlDbType = SqlDbType.Real;
                                if(parameter.Direction == ParameterDirection.Input  || parameter.Direction == ParameterDirection.InputOutput )parameter.Value = InputConsole.InputReal($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "nvarchar":
                            {
                                parameter.SqlDbType = SqlDbType.NVarChar;
                                if (parameter.Direction == ParameterDirection.Input || parameter.Direction == ParameterDirection.InputOutput)
                                {
                                    parameter.Value = InputConsole.InputText($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                }                                    
                                break;
                            }
                        case "nchar":
                            {
                                parameter.SqlDbType = SqlDbType.NChar;
                                if(parameter.Direction == ParameterDirection.Input  || parameter.Direction == ParameterDirection.InputOutput )parameter.Value = InputConsole.InputNchar($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "money":
                            {
                                parameter.SqlDbType = SqlDbType.Money;
                                if(parameter.Direction == ParameterDirection.Input  || parameter.Direction == ParameterDirection.InputOutput )parameter.Value = InputConsole.InputMoney($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "int":
                            {
                                parameter.SqlDbType = SqlDbType.Int;
                            parameter.Value = 0;
                                if (parameter.Direction == ParameterDirection.Input  || parameter.Direction == ParameterDirection.InputOutput )parameter.Value = InputConsole.InputInt($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "image":
                            {
                                parameter.SqlDbType = SqlDbType.Image;
                                if(parameter.Direction == ParameterDirection.Input  || parameter.Direction == ParameterDirection.InputOutput )parameter.Value = InputConsole.InputImage($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "float":
                            {
                                parameter.SqlDbType = SqlDbType.Float;
                                if(parameter.Direction == ParameterDirection.Input  || parameter.Direction == ParameterDirection.InputOutput )parameter.Value = InputConsole.InputFloat($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "decimal":
                            {
                                parameter.SqlDbType = SqlDbType.Decimal;
                                if(parameter.Direction == ParameterDirection.Input  || parameter.Direction == ParameterDirection.InputOutput )parameter.Value = InputConsole.InputDecimal($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        case "datetime":
                            {
                                parameter.SqlDbType = SqlDbType.DateTime;
                                if(parameter.Direction == ParameterDirection.Input  || parameter.Direction == ParameterDirection.InputOutput )parameter.Value = InputConsole.InputDateTime($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                            //
                        case "char":
                            {
                                parameter.SqlDbType = SqlDbType.Char;
                                if(parameter.Direction == ParameterDirection.Input  || parameter.Direction == ParameterDirection.InputOutput )parameter.Value = InputConsole.InputChar($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
                        // 0 или 1
                        case "bit": 
                            {
                                parameter.SqlDbType = SqlDbType.Bit;
                                if(parameter.Direction == ParameterDirection.Input  || parameter.Direction == ParameterDirection.InputOutput )parameter.Value = InputConsole.InputBool($"{kv.Key} {kv.Value.DataType}", null, ref args);
                                break;
                            }
  
                        default: throw new NotSupportedException($"Тип данных {parameterMetadata.DataType.ToCapitalStyle()}");
                    }
                    arguments[parameter.ParameterName] = parameter?.Value?.ToString();
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
