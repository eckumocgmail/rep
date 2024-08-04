using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataODBC
{
    public class OdbcRouterComponent 
    {

        public async Task InvokeAsync( HttpContext http )
        {
            await Task.CompletedTask;
            string path = http.Request.Path.ToString();
            if (path.StartsWith("/"))
            {
                path = path.Substring(1);
            }


            foreach( var name in path.Split('/').ToList())
            {

            }

        }
    }
}
