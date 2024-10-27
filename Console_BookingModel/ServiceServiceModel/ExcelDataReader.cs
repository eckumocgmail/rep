using System.Data.OleDb;
using System.Data;

namespace Console_BookingUI.Data
{
    public class ExcelDataReader
    {
        public Dictionary<string, Tuple<List<string>, IEnumerable<IDictionary<string, string>>>> ReadXlsFile(string filename)
        {
            string ConnectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties=\"Excel 8.0;HDR=No;IMEX=1\"; Data Source={0}", filename);
            DataSet ds = new DataSet("EXCEL");

            Dictionary<string, Tuple<List<string>, IEnumerable<IDictionary<string, string>>>> result = new();
            OleDbConnection cn = new OleDbConnection(ConnectionString);
            cn.Open();

            var schema = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            var columns = GetColumnsNames(schema).ToList();
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

                columns = GetColumnsNames(tb).ToList();
                data = GetTextData(tb);
                result[table] = new(columns, data);

            }
            return result;
        }
        public Dictionary<string, Tuple<List<string>, IEnumerable<IEnumerable<string>>>> ReadXlsFileAsList(string filename, int sheetNumber = -1, int columnsInRow = 0)
        {
            string ConnectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties=\"Excel 8.0;HDR=No;IMEX=1\"; Data Source={0}", filename);
            DataSet ds = new DataSet("EXCEL");

            Dictionary<string, Tuple<List<string>, IEnumerable<IEnumerable<string>>>> result = new();
            OleDbConnection cn = new OleDbConnection(ConnectionString);
            cn.Open();

            var schema = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            var columns = GetColumnsNames(schema).ToList();
            var data = GetTextData(schema);
            var tables = data.Select(item => item["TABLE_NAME"]);

            cn.Info(columns.ToJsonOnScreen());
            cn.Info(data.ToJsonOnScreen());
            if (sheetNumber == -1)
            {
                var dataset = new DataSet();
                foreach (var table in tables)
                {
                    string select = String.Format("SELECT * FROM [{0}]", table);
                    OleDbDataAdapter ad = new OleDbDataAdapter(select, cn);
                    ad.Fill(ds);
                    DataTable tb = ds.Tables[0];

                    columns = GetColumnsNames(tb).ToList();
                    var datalist = GetTextDataList(tb);
                    if (columnsInRow != 0)
                    {
                        columns = datalist[columnsInRow].ToList();
                        while (columnsInRow >= 0)
                        {
                            datalist.RemoveAt(columnsInRow);
                            columnsInRow--;
                        }
                    }
                    result[table] = new(columns, datalist);

                }
            }
            else
            {
                sheetNumber = sheetNumber < 0 || sheetNumber >= tables.Count() ? 0 : sheetNumber;
                var table = tables.ToList()[sheetNumber];
                string select = String.Format("SELECT * FROM [{0}]", table);
                OleDbDataAdapter ad = new OleDbDataAdapter(select, cn);
                ad.Fill(ds);
                DataTable tb = ds.Tables[0];

                columns = GetColumnsNames(tb).ToList();
                var datalist = GetTextDataList(tb);
                if (columnsInRow != 0)
                {
                    columns = datalist[columnsInRow].ToList();
                    while (columnsInRow >= 0)
                    {
                        datalist.RemoveAt(columnsInRow);
                        columnsInRow--;
                    }
                }
                result[table] = new(columns, datalist);
            }
            return result;
        }
        public IEnumerable<string> GetColumnsNames(DataTable dataTable)
        {
            List<string> columnNames = new List<string>();

            foreach (DataColumn column in dataTable.Columns)
            {
                columnNames.Add(column.ColumnName);
            }
            return columnNames.ToArray();
        }
        public List<IEnumerable<string>> GetTextDataList(DataTable dataTable, int startRow = -1)
        {
            int nRow = 0;
            var result = new List<IEnumerable<string>>();
            foreach (DataRow row in dataTable.Rows)
            {
                if (startRow != -1 && nRow < startRow)
                {
                    nRow++;
                    continue;
                }
                List<string> data = new List<string>();
                foreach (DataColumn column in dataTable.Columns)
                {
                    object value = row[column.ColumnName];
                    data.Add(value is not null ? value.ToString() : null);
                }
                result.Add(data);
                nRow++;
            }
            return result;
        }

        public IEnumerable<IDictionary<string, string>> GetTextData(DataTable dataTable, int startRow = -1)
        {
            int nRow = 0;
            var result = new List<IDictionary<string, string>>();
            foreach (DataRow row in dataTable.Rows)
            {
                if (startRow != -1 && nRow < startRow)
                {
                    nRow++;
                    continue;
                }
                IDictionary<string, string> data = new Dictionary<string, string>();
                foreach (DataColumn column in dataTable.Columns)
                {
                    object value = row[column.ColumnName];
                    data[column.ColumnName] = value == null ? "NULL" : value.ToString();
                }
                result.Add(data);
                nRow++;
            }
            return result.ToArray();
        }
    }
}
