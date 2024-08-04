using Console_DataConnector.DataModule.DataADO.ADOWebApiService;

using System;

namespace Console_DataConnector
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new SqlServerWebApi();
            api.Info($"GetTableNames()");
            api.Info(api.GetProceduresMetadata());
            api.Info(api.GetEntitiesTypes());
            api.AddEntityType(typeof(Program));             
            api.UpdateDatabase();

            BusinessProgram.Run(args);
            //DataProgram.Run(args);  
        }
    }
}
