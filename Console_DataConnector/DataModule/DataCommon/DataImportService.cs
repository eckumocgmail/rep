using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class DataImportService
{
    public static IEnumerable<string> GetColumnsNames(DataTable dataTable)
    {
        List<string> columnNames = new List<string>();
        foreach (DataColumn column in dataTable.Columns)
        {
            columnNames.Add(column.ColumnName);
        }
        return columnNames.ToArray();
    }
    public static IEnumerable<IDictionary<string, string>> GetTextData(DataTable dataTable)
    {
        var result = new List<IDictionary<string, string>>();
        foreach (DataRow row in dataTable.Rows)
        {
            IDictionary<string, string> data = new Dictionary<string, string>();
            foreach (DataColumn column in dataTable.Columns)
            {
                object value = row[column.ColumnName];
                data[column.ColumnName] = value == null ? "NULL" : value.ToString();
            }
            result.Add(data);
        }
        return result.ToArray();
    }
    public static DataSet ReadExcelFile( string filename )
    {

        
        
        //string filename = @"D:\System-Config\MyExpirience\Console_BlazorApp\Console_BookingModel\Resources\works.xlsx";


        string ConnectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties=\"Excel 8.0;HDR=No;IMEX=1\"; Data Source={0}", filename);
        DataSet ds = new DataSet("EXCEL");

        OleDbConnection cn = new OleDbConnection(ConnectionString);
        cn.Open();

        var schema = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        var columns = GetColumnsNames(schema);
        var data = GetTextData(schema);
        var tables = data.Select(item => item["TABLE_NAME"]);

        cn.Info(columns.ToJsonOnScreen());
        cn.Info(data.ToJsonOnScreen());

        var dataset = new DataSet();
        foreach (var table in tables)
        {
            string select = String.Format("SELECT * FROM [{0}]", table);
            OleDbDataAdapter ad = new OleDbDataAdapter(select, cn);
            ad.Fill(ds);
            DataTable tb = ds.Tables[0];
            return tb.DataSet;
            dataset.Tables.Add(tb);
        }


        return dataset;
    }
}
