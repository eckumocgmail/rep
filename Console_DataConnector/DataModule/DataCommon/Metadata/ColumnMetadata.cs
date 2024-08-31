using Console_DataConnector.DataModule.DataCommon.DatabaseMetadata;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Console_DataConnector.DataModule.DataCommon.Metadata
{
    public class ColumnMetadata : MyValidatableObject
    {

        public ColumnMetadata() { }
        public ColumnMetadata(ColumnMetaData columnMetaData)
        {
            DataType = columnMetaData.type;
            ColumnName = columnMetaData.name;
        }

        public int Id { get; set; }

        //[NotNullNotEmpty]
        public string TableCatalog { get; set; }

        //[NotNullNotEmpty]
        public string TableSchema { get; set; }

        //[NotNullNotEmpty]
        public string TableName { get; set; }

        //[NotNullNotEmpty]
        public string ColumnName { get; set; }

        //[InputNumber]
        //[IsPositiveNumber]
        //[NotNullNotEmpty]
        public int OrdinalPosition { get; set; }
        public bool IsComputed { get; set; } = false;
        public string IsNullable { get; set; }
        public string DataType { get; set; }

        //[Label("Параметр сортировки")]
        public string CollationName { get; set; }


        public string CharacterSetName { get; set; }

    }
}
