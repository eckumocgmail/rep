using Console_DataConnector.DataModule.DataCommon.DatabaseMetadata;
using System;

namespace Console_DataConnector.DataModule.DataADO.ADODbMigBuilderService
{
    public interface IdDLFactory
    {
        string CreateForeignkey(string relativeTable, string table, string column, bool? onDeleteCascade = false, bool? onUpdateCascade = null);
        string CreateTable(Type metadata);
        TableMetaData CreateTableMetaData(Type metadata);
        string CreateTable(TableMetaData metadata);
    }
}