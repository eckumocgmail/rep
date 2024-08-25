using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataADO.ADOWebApiService
{
    public interface IWebApi
    {
        Task<Tuple<int, object>> Request(string url, Dictionary<string, string> queryParams, Dictionary<string, string> headers, byte[] body);        
    }
}
