using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



using Console_DataConnector.DataModule.DataApi;
using Console_DataConnector.DataModule.DataCommon.DatabaseMetadata;
using Console_DataConnector.DataModule.DataODBC.Manager;

namespace Console_DataConnector.DataModule.DataCommon.DataTypes
{
    public class TableManagerStatefull : MyValidatableObject, ITableManagerStatefull
    {
        public ITableManager tableManager;
        public JArray dataRecords;
        public OdbcDatabaseManager databaseManager;

        public TableManagerStatefull()
        {

        }
        public TableManagerStatefull(ITableManager tm)
        {
            tableManager = tm;
            dataRecords = tableManager.SelectAll();
            //tableManager.SelectMaxId();
        }


        /**
         * выбор обьектов ссылающихся на запись в заданной таблице с заданным идентификатором
         */
        public object SelectReferencesFrom(string table, long record_id)
        {

            string fk = (from p in tableManager.GetMetadata().fk where p.Value.ToLower() == table.ToLower() select p.Key).SingleOrDefault();
            return (from r in dataRecords where r[fk].Value<long>() == record_id select r).ToList();
        }


        /**
         * выбор обьектов ссылающихся на запись в заданной таблице с заданным идентификатором
         */
        public object SelectReferencesTo(string table, long record_id)
        {
            OdbcTableManager tableRef = databaseManager.GetFasade()[table];
            return new List<object>();
        }


        /**
         * выбор обьектов ссылающихся на запись в заданной таблице с заданным идентификатором
         */
        public object SelectNotReferencesTo(string table, long record_id)
        {
            string fk = (from p in tableManager.GetMetadata().fk where p.Value.ToLower() == table.ToLower() select p.Key).SingleOrDefault();
            return (from r in dataRecords where r[fk].Value<long>() == record_id select r).ToList();
        }


        public TableManagerStatefull(OdbcDatabaseManager databaseManager, OdbcTableManager tableManager)
        {
            this.databaseManager = databaseManager;
            this.tableManager = tableManager;
            dataRecords = this.tableManager.SelectAll();
            //this.tableManager.SelectMaxId();
        }


        public long Count()
        {
            ///return this.dataRecords.Count();
            return tableManager.Count();
        }


        public OdbcTableManager Get(string table)
        {
            return databaseManager.Get(table);
        }


        public TableMetaData GetMetadata()
        {
            return tableManager.GetMetadata();
        }


        public JArray SelectAll()
        {
            return dataRecords;
        }


        public object WhereColumnValueIn(string column, JArray values)
        {
            List<long> valuesList = new List<long>();
            foreach (JValue val in values)
            {
                valuesList.Add(val.Value<long>());
            }
            return (from rec in dataRecords where valuesList.Contains(rec[column].Value<long>()) select rec).ToList();
        }


        public JToken Select(long id)
        {
            TableMetaData metadata = GetMetadata();
            string pk = metadata.getPrimaryKey();
            if (pk == null)
            {
                throw new NullReferenceException(pk);
            }
            if (true) Console.WriteLine(dataRecords);
            return (from r in dataRecords where r[pk].Value<long>() == id select r).SingleOrDefault();
        }

        public JArray Search(List<string> terms, string query)
        {
            JArray arr = new JArray();

            foreach (var record in dataRecords)
            {
                foreach (string term in terms)
                {
                    if (record[term].Value<string>().IndexOf(query) != -1)
                    {
                        arr.Add(record);
                        break;
                    }
                }

            }
            return arr;
        }

        public JArray Join(long id, string table)
        {
            throw new Exception("unsupported");
            //return this.datasource.Execute( "select * from " + metadata.name + " where " + metadata.pk + " = " + id );
        }


        public JArray SelectPage(long page, long size)
        {
            JArray arr = new JArray();
            JObject[] records = dataRecords.Values<JObject>().ToArray();
            for (long i = page * size; i < (page + 1) * size; i++)
            {
                if (i < records.Length)
                {
                    arr.Add(records[i]);
                }
            }
            return arr;
        }


        public long Create(Dictionary<string, object> values)
        {
            try
            {
                dataRecords.Add(JObject.FromObject(values));
                tableManager.Create(values);
                return 1;
            }
            catch (Exception ex)
            {
                this.Error(ex);
                return -1;
            }
        }


        public long Update(Dictionary<string, object> values)
        {
            if (GetMetadata().pk == null)
            {
                throw new Exception("primary key not defined");
            }
            if (!values.ContainsKey(GetMetadata().pk))
            {
                throw new Exception("values argument has not primary key identifier");
            }
            long objectId = long.Parse(values[GetMetadata().pk].ToString());

            JToken record = Select(objectId);
            JObject jvalues = JObject.FromObject(values);
            foreach (var p in values)
            {
                record[p.Key] = jvalues[p.Key];
            }
            new Task(() => { tableManager.Update(values); }).Start();
            return 1;
        }


        public long Delete(long id)
        {
            dataRecords.Remove(Select(id));
            new Task(() => { tableManager.Delete(id); }).Start();
            return 1;
        }


        public object GetAssociations(string key)
        {
            switch (key)
            {
                case "$list": return GetCommands();
                case "$metadata": return GetMetadata();
                case "$popular": return GetPopular();
                //case "$latest": return this.GetLatest();
                // case "$rating": return this.GetRating();
                //  case "$stats": return this.GetStats();
                case "$keywords": return GetKeywords();
                case "$errors": return GetErrors();
                default: return null;
            }
        }

        public object GetCommands()
        {
            return new string[] { "$stats", "$keywords", "$errors", "$rating", "$latest", "$popular", "$metadata", "$list" };
        }

        public object GetErrors()
        {
            return null;
        }


        public object GetPopular()
        {
            return null;
        }


        IDictionary<string, int> keywords { get; set; }
        public IDictionary<string, int> GetKeywords()
        {
            keywords = new Dictionary<string, int>();
            string name = GetMetadata().name;
            TableMetaData metadata = ((OdbcTableManager)tableManager).GetMetadata();
            string pk = //this.GetMetaData().Tables.ContainsKey(metadata.singlecount_name) ?
                        //this.GetMetaData().Tables[metadata.singlecount_name].getPrimaryKey() :
                        //this.GetMetaData().Tables[metadata.name].getPrimaryKey();
                        tableManager.GetMetadata().getPrimaryKey();

            if (pk == null)
            {
                throw new Exception("Primary key udefined for table " + name);
            }
            var tms = new TableManagerStatefull((OdbcTableManager)tableManager);
            List<string> textColumns = ((OdbcTableManager)tableManager).GetMetadata().GetTextColumns();
            foreach (JObject record in tms.dataRecords)
            {
                try
                {
                    Console.WriteLine(record);
                    int id = record[pk].Value<int>();

                    Dictionary<string, int> statisticsForThisRecord = new Dictionary<string, int>();
                    foreach (string column in textColumns)
                    {
                        if (record[column] != null)
                        {
                            Console.WriteLine(record.ToString());
                            string textValue = record[column].Value<string>();
                            if (string.IsNullOrEmpty(textValue)) continue;
                            foreach (string word in textValue.Split(" "))
                            {
                                bool isRus = word.IsRus();
                                bool isEng = word.IsEng();
                                if (isRus == false && isEng == false)
                                    continue;
                                if (keywords.ContainsKey(word))
                                {
                                    keywords[word]++;
                                }
                                else
                                {
                                    keywords[word] = 1;
                                }

                                if (statisticsForThisRecord.ContainsKey(word))
                                {
                                    statisticsForThisRecord[word]++;
                                }
                                else
                                {
                                    statisticsForThisRecord[word] = 1;
                                }
                            }
                        }
                    }

                    //statistics[name + "/" + id] = statisticsForThisRecord;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    continue;
                }
            }
            return keywords;

        }
    }
}
