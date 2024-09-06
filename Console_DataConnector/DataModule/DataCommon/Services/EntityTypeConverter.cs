﻿using Console_DataConnector.DataModule.DataCommon.DatabaseMetadata;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataCommon.Services
{
    class SqlFactory
    {
        public TableMetaData GetTableMetaDataFor(Type typeofEntity)
        {
            TableMetaData tmd = new TableMetaData();
            tmd.name = typeofEntity.Name;
            tmd.columns = new Dictionary<string, ColumnMetaData>();
            foreach (var p in typeofEntity.GetProperties())
            {
                tmd.columns[p.Name] = new ColumnMetaData()
                {
                    primary = p.Name == "Id",
                    caption = Utils.LabelFor(typeofEntity, p.Name),
                    description = Utils.DescriptionFor(typeofEntity, p.Name),
                    name = p.Name,
                    unique = Utils.IsUniq(typeofEntity, p.Name),
                    cstype = p.PropertyType.GetTypeName(),
                    type = Utils.GetInputType(typeofEntity, p.Name)



                };
            }
            return tmd;
        }


        /// <summary>
        /// Создать внешний ключ
        /// </summary>
        /// <returns></returns>
        public string CreateForeignkey(string relativeTable, string table, string column, bool? onDeleteCascade = false, bool? onUpdateCascade = null)
        {
            return $"ALTER TABLE {table} ADD CONSTRAINT {table + relativeTable} FOREIGN KEY {column} REFERENCES ({relativeTable})" + " ON DELETE " + (onDeleteCascade == true ? "CASCADE" : "NO ACTION ") + " ON UPDATE " + (onUpdateCascade == true ? "CASCADE" : "NO ACTION ");
        }


        /// <summary>
        /// Создать таблицу
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public string CreateTable(TableMetaData metadata)
        {
            string sql = $"CREATE TABLE [{metadata.schema}].[{metadata.name}]\n";
            sql += "(";
            foreach (var p in metadata.columns)
            {
                sql += $"\n  {p.Key} {p.Value.type} ";
                if (p.Key.ToLower() == metadata.pk.ToLower())
                {
                    if (p.Value.incremental)
                    {
                        sql += " IdENTITY(1,1) ";
                    }
                    sql += " PRIMARY KEY,";
                }
                else
                {
                    sql += ",";
                }
            }
            if (metadata.columns.Count > 0)
            {
                sql = sql.Substring(0, sql.Length - 1);
            }
            sql += "\n)\n";
            return sql;
        }

        public string CreateTable(Type metadata)
        {
            var tableMetaData = GetTableMetaDataFor(metadata);
            return CreateTable(tableMetaData);
        }
    }
    class EntityTypeConverter
    {

        public TableMetaData GetTableMetaDataFor(Type typeofEntity)
        {
            TableMetaData tmd = new TableMetaData();
            tmd.name = typeofEntity.Name;
            tmd.columns = new Dictionary<string, ColumnMetaData>();
            foreach (var p in typeofEntity.GetProperties())
            {
                tmd.columns[p.Name] = new ColumnMetaData()
                {
                    primary = p.Name == "Id",
                    caption = Utils.LabelFor(typeofEntity, p.Name),
                    description = Utils.DescriptionFor(typeofEntity, p.Name),
                    name = p.Name,
                    unique = Utils.IsUniq(typeofEntity, p.Name),
                    cstype = p.PropertyType.GetTypeName(),
                    type = Utils.GetInputType(typeofEntity, p.Name)



                };
            }
            return tmd;
        }
    }
}
