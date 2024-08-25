using Console_DataConnector.DataModule.DataADO.ADOWebApiService;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Console_DataConnector
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //DataProgram.Run(args);

            var api = new SqlServerWebApi();
            foreach(var p in api.GetProceduresMetadata())
            {
                var input = new Dictionary<string,string>(p.Value.ParametersMetadata.Keys.Select(key => new KeyValuePair<string, string>(key, "1")));
                api.Info(input.ToJsonOnScreen());
                args.Info(api.Request($"/{p.Key}", input).Result);
            }
            
            //api.Info(api.GetProceduresMetadata());            
            /*api.Info(api.GetEntitiesTypes());
            api.AddEntityType(typeof(Program));             
            api.UpdateDatabase();*/

            //BusinessProgram.Run(args);
           
        }
    }
}
