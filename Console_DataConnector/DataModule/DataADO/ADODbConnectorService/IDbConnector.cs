using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataADO.ADODbConnectorService
{
    public interface IdbConnector<TDBConnection> where TDBConnection : DbConnection
    {
        public TDBConnection CreateAndOpenConnection();
        public TDBConnection GetConnection();
    }
}


