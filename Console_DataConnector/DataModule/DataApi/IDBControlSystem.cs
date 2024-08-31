using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataApi
{
    public interface IdBControlSystem
    {
        public IEnumerable<string> GetDatabases(string connectionString);
        public IEnumerable<string> GetDatabases();
    }
}
