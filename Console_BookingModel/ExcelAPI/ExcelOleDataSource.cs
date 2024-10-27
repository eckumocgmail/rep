
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ExcelOleDataSource:OleDataSource
{

    public static Dictionary<string,object> ReadFile( string filename )
    {
        Dictionary<string, object> db = new Dictionary<string, object>();
        ExcelOleDataSource ds = new ExcelOleDataSource(filename);
        foreach (JObject table in ds.GetTablesMetadata())
        {
            db[table["TABLE_NAME"].Value<string>()] = ds.execute("select * from [" + table["TABLE_NAME"].Value<string>() + "]");
        }
        return db;
    }


    public ExcelOleDataSource(string filename):base(DataSourceEnumeration.Excel, filename)
    {
        if( !System.IO.File.Exists(filename))
        {
            throw new Exception("file:"+filename+" not exist");
        }
    }
}

