


using Console_ApiTests;

using Console_DataConnector.DataModule.DataADO.ADODbConnectorService;
using Console_DataConnector.DataModule.DataADO.ADODbExecutorService;
using Console_DataConnector.DataModule.DataADO.ADODbMetadataServices;
using Console_DataConnector.DataModule.DataADO.ADODbMigBuilderService;
using Console_DataConnector.DataModule.DataADO.ADOWebApiService;
using Console_DataConnector.DataModule.DataModels.MessageService;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Console_DataConnector
{
    public class Program
    {
        public static void Main( string[] args)
        {
            DataImportService.ReadExcelFile(@"D:\System-Config\MyExpirience\Console_BlazorApp\Console_BookingModel\Resources\works.xlsx").WriteToConsole(); ;

            args.Info(Assembly.GetExecutingAssembly().GetName().Name);
            args.Info(Assembly.GetCallingAssembly().GetName().Name);

            Console_InputApplication.Program.Main(args);
            ServiceFactory.Get().AddTypes(typeof(Program).Assembly);

            if (args.Contains("http"))
            {
                Host.CreateDefaultBuilder()
                    .ConfigureWebHostDefaults(webBuilder =>
                        webBuilder.UseStartup<Program>())
                    .Build().Run();
            }
            else
            {
                if(Assembly.GetExecutingAssembly().GetName().Name == Assembly.GetCallingAssembly().GetName().Name)
                {
                    Action todo = InputConsole.SwitchContinue("Меню", new Dictionary<string, Action>() {
                        { "Базы данных", ()=>OdbcProgram.Run(ref args) },
                        { "Приложение",  ()=>DataProgram.Run(ref args)  },
                        { "Выход", ()=>InputConsole.Exit() }
                    }, ref args);
                        todo();
                }
                



            }
        }

        public void Configure(ref string[] args)
        {
            if (InputConsole.Confirm("Запустить тесты ADO?"))
            {
                var unit = new ConsoleDataConnectorUnit();
                unit.Test();
            }
            if (InputConsole.Confirm("Выполнить программу ODBC?"))
            {
                OdbcProgram.Run(ref args);
            }
            if (InputConsole.Confirm("Выполнить программу управления данными?"))
            {
                DataProgram.Run(ref args);
            }
            if (InputConsole.Confirm("Проверить ADO объекты?"))
            {
                ADODbConnectorsTest.Run();
                ADOExecutorTest.Run();
                ADODbMetadataTest.Run();
                ADODbMigBuilderTest.Run();
            }
            if (InputConsole.Confirm("Проверить генерацию структуры данных?"))
            {
                var api = new MessageWebApi();
                api.Info(api.GetProceduresMetadata());
                api.Info(api.GetEntitiesTypes());
                api.AddEntityType(typeof(Program));
                api.UpdateDatabase();
            }
            if (InputConsole.Confirm("Запустить тесты?"))
            {
                InputConsole.Info(new MessageWebApiTest().DoTest().ToDocument());
            }
        }
    }
}
