using Console_ApiTests;

using Console_DataConnector.DataModule.DataADO.ADODbConnectorService;
using Console_DataConnector.DataModule.DataADO.ADODbExecutorService;
using Console_DataConnector.DataModule.DataADO.ADODbMetadataServices;
using Console_DataConnector.DataModule.DataADO.ADODbMigBuilderService;
using Console_DataConnector.DataModule.DataADO.ADOWebApiService;
using Console_DataConnector.DataModule.DataModels.MessageModel;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Console_DataConnector
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new ConsoleDataConnectorUnit().Test();
            ADODbConnectorsTest.Run();
            ADOExecutorTest.Run();
            ADODbMetadataTest.Run();
            ADODbMigBuilderTest.Run();
          
            //DataProgram.Run(args);

            /*var api = new MessageWebApi();
            api.Info(api.GetProceduresMetadata());
            api.Info(api.GetEntitiesTypes());
            api.AddEntityType(typeof(Program));             
            api.UpdateDatabase();
            foreach (var p in api.GetProceduresMetadata())
            {
                var input = new Dictionary<string,string>(p.Value.ParametersMetadata.Keys.Select(key => new KeyValuePair<string, string>(key, "1")));
                api.Info(input.ToJsonOnScreen());
                args.Info(api.Request($"/{p.Key}", input).Result);
            }*/

            new MessageWebApiTest().DoTest().ToDocument().WriteToConsole();

            //*/

            //BusinessProgram.Run(args);

        }
    }
}
