using Console_DataConnector.DataModule.DataCommon.DatabaseMetadata;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Console_DataConnector.DataModule.DataApi
{
    public interface ITableManager
    {
        int Count();
        int Create();
        int Create(Dictionary<string, object> values);
        int Delete(long id);
        List<string> GetKeywords(string entity, string keywordsQuery);
        TableMetaData GetMetadata();
        JArray Join(long id, string table);
        JArray Search(string entity, string searchedQuery);
        JArray Select(int id);
        JArray Select(long id);
        JArray SelectAll();
        int SelectMaxId();
        JArray SelectPage(long page, long size);
        int Update(Dictionary<string, object> values);
    }
}