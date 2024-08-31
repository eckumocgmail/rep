using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataApi
{
    public interface IdataStorage
    {

        public void RestoreDatabaseFrom(
                string ConnectionString,
                string FilePath,
                bool FullMode);
        public void BackupDatabaseToFile(
                string ConnectionString,
                string FilePath,
                bool FullMode);
    }
}