using Console_ApiTests;

using Console_DataConnector.DataModule.DataADO.ADODbConnectorService;
using Console_DataConnector.DataModule.DataADO.ADODbExecutorService;
using Console_DataConnector.DataModule.DataADO.ADODbMetadataServices;
using Console_DataConnector.DataModule.DataADO.ADODbMigBuilderService;
using Console_DataConnector.DataModule.DataADO.ADOWebApiService;
using Console_DataConnector.DataModule.DataModels.MessageService;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Console_DataConnector
{
    public class Program
    {
        public static void Main(string[] args)
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
            if(InputConsole.Confirm("Проверить генерацию структуры данных?"))
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
