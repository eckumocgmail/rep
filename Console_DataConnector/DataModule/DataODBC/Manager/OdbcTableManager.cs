using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Console_DataConnector.DataModule.DataApi;
using Console_DataConnector.DataModule.DataCommon.DatabaseMetadata;

namespace Console_DataConnector.DataModule.DataODBC.Manager
{
    public class OdbcTableManager : MyValidatableObject, ITableManager
    {
        [NotNullNotEmpty]
        private APIdataSource datasource { get; set; }

        [NotNullNotEmpty]
        private TableMetaData metadata { get; set; }


        public OdbcTableManager() { }

        public OdbcTableManager(string name, APIdataSource datasource, TableMetaData metadata)
        {
            this.datasource = datasource;
            this.metadata = metadata;

        }

        public int SelectMaxId()
        {
            this.EnsureIsValide();
            metadata.EnsureIsValide();
            JObject record = datasource.GetSingleJObject("select max(" + metadata.pk + ") as MaxId from " + metadata.name);
            return 0;// record == null || record.Count == 0 || record["MaxId"] == null? 0 : record["MaxId"].Value<int>();
        }

        public int Count()
        {
            JObject record = datasource.GetSingleJObject("select count(*) as RecordsCount from " + metadata.name);
            return record == null || record.Count == 0 ? 0 : record["RecordsCount"].Value<int>();
        }

        public TableMetaData GetMetadata()
        {
            return metadata;
        }

        public JArray SelectAll()
        {
            return datasource.GetJsonResult("select * from " + metadata.name);
        }

        public JArray Select(int id)
        {
            return datasource.GetJsonResult("select * from " + metadata.name + " where " + metadata.pk + " = " + id);
        }

        public JArray Select(long id)
        {
            return datasource.GetJsonResult("select * from " + metadata.name + " where " + metadata.pk + " = " + id);
        }

        public JArray Join(long id, string table)
        {
            return datasource.GetJsonResult("select * from " + metadata.name + " where " + metadata.pk + " = " + id);
        }

        public JArray SelectPage(long page, long size)
        {
            return datasource.GetJsonResult("select * from " + metadata.name + " limit " + page + " , " + size);
        }


        /// <summary>
        /// Метод добавление данных в базу
        /// </summary>
        /// <param name="values"> значения </param>
        /// <returns></returns>
        public int Create()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            foreach (var p in metadata.columns)
            {
                object value = null;
                switch (p.Value.type.ToLower())
                {
                    case "int": value = 10; break;
                    case "float": value = 0.5f; break;
                    case "varchar": value = "test"; break;
                    case "nvarchar": value = "test"; break;
                    case "date": value = DateTime.Now; break;
                    case "datetime": value = DateTime.Now; break;
                    case "datetime2": value = DateTime.Now; break;
                    case "ntext": value = "test"; break;
                    case "int identity": continue;
                    default: throw new Exception($"Тип данных {p.Value.type} не поддерживается. Регистрация записей в {metadata.name} невозможен");
                }
                values[p.Key] = value;
            };
            return Create(values);
        }
        public int Create(Dictionary<string, object> values)
        {
            string keys = "";
            string valuesstr = "";
            if (!metadata.ContainsBlob())
            {
                foreach (var p in metadata.columns)
                {
                    if (values.ContainsKey(p.Key))
                    {
                        keys += p.Key + ",";
                        valuesstr += toSqlValueStr(p.Key, values[p.Key]) + ",";
                    }
                };
                keys = keys.Substring(0, keys.Length - 1);
                valuesstr = valuesstr.Substring(0, valuesstr.Length - 1);
                string sql = "insert into " + metadata.name + "(" + keys + ") values (" + valuesstr + ")";
                datasource.GetJsonResult(sql);
                return 0;
                //return this.datasource.ExecuteSingle(" SELECT LAST_INSERT_Id( ) as Id;")["Id"].Value<int>();
            }
            else
            {
                byte[] data = null;
                foreach (var p in metadata.columns)
                {
                    if (values.ContainsKey(p.Key))
                    {
                        keys += p.Key + ",";
                        if (metadata.columns[p.Key].type.ToLower() != "blob")
                        {
                            valuesstr += toSqlValueStr(p.Key, values[p.Key]) + ",";
                        }
                        else
                        {
                            valuesstr += "?,";
                            string binaryString = values[p.Key].ToString();
                            data = new byte[binaryString.Length];
                            for (int i = 0; i < binaryString.Length; i++)
                            {
                                data[i] = (byte)binaryString[i];
                            }
                        }

                    }
                };
                keys = keys.Substring(0, keys.Length - 1);
                valuesstr = valuesstr.Substring(0, valuesstr.Length - 1);
                string sql = "insert into " + metadata.name + "(" + keys + ") values (" + valuesstr + ")";
                datasource.InsertBlob(sql, "@bin_data", data);
                return 0;
                //return this.datasource.ExecuteSingle(" SELECT LAST_INSERT_Id( ) as Id;")["Id"].Value<int>();

            }
        }

        public JArray Search(string entity, string searchedQuery)
        {
            throw new NotImplementedException();
        }

        public List<string> GetKeywords(string entity, string keywordsQuery)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="values"> значения </param>
        /// <returns></returns>
        public int Update(Dictionary<string, object> values)
        {
            string setup = "";
            foreach (var p in values)
            {
                if (p.Key == metadata.pk) continue;
                string value = toSqlValueStr(p.Key, p.Value);

                setup += p.Key + "=" + value + ",";
            }
            setup = setup.Substring(0, setup.Length - 1);
            datasource.GetJsonResult("update " + metadata.name + " set " + setup + " where " + metadata.pk + " = " + values[metadata.pk]);
            return 1;
        }


        /// <summary>
        /// Преобразование данных к формату SQL
        /// </summary>
        /// <param name="name"> имя атрибута сущности </param>
        /// <param name="val"> значение </param>
        /// <returns></returns>
        private string toSqlValueStr(string name, object val)
        {



            if (val == null || val.ToString() == "NULL")
            {
                return "NULL";
            }
            string value = "";
            string typestr = metadata.columns[name].type.ToLower();
            switch (typestr)
            {
                case "date":
                    DateTime date = (DateTime)val;
                    value =
                        "DateFromParts(" + date.Year + "," + date.Month + "," + date.Day + ")";
                    break;
                case "datetime":
                    DateTime datetime = (DateTime)val;
                    value =
                        "DateTimeFromParts(" + datetime.Year + "," + datetime.Month + "," + datetime.Day +
                        $",{datetime.Hour},{datetime.Minute},{datetime.Second},0)";
                    break;
                case "datetime2":
                    DateTime datetime2 = (DateTime)val;
                    value =
                        "DateTimeFromParts(" + datetime2.Year + "," + datetime2.Month + "," + datetime2.Day +
                        $",{datetime2.Hour},{datetime2.Minute},{datetime2.Second},0)";
                    break;
                /*case "date":
                    DateTime date = (DateTime)val;
                    value = "'" + date.Year + "-" + date.Month + "-" + date.Day + "'";
                    break;
                case "datetime":
                    DateTime datetime = (DateTime)val;
                    value = 
                        "'" + datetime.Year + "-" + datetime.Month + "-" + datetime.Day + 
                        $" {datetime.Hour}:{datetime.Minute}:{datetime.Second}'";
                    break;*/
                case "ntext":
                case "text":
                case "nvarchar":
                case "varchar":
                    value = "'" + val.ToString().Replace("'", "\"") + "'";
                    break;
                case "smallint":
                case "int":
                case "float":
                    value = "" + val.ToString().Replace(",", ".") + "";
                    break;
                default:
                    throw new Exception("Тип данных " + typestr + " пока не поддерживается");
            }

            return value;
        }


        /// <summary>
        /// Удаление записи с заданным идентификатором
        /// </summary>
        /// <param name="id">идентификатор записи</param>
        /// <returns></returns>
        public int Delete(long id)
        {
            if (string.IsNullOrEmpty(metadata.pk))
            {
                throw new Exception("primary key not defined at table metadata");
            }
            datasource.GetJsonResult("delete from " + metadata.name + " where " + metadata.pk + " = " + id);
            return 1;
        }

    }
}
