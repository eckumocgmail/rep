﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataApi
{
    public interface ISqlConnection
    {
        public string GetConnectionString();
    }

    public interface ISqlConnectionService
    {
        public bool CanConnect(string ConnectionString);
        public IEnumerable<string> GetTables(string ConnectionString);
    }
}
