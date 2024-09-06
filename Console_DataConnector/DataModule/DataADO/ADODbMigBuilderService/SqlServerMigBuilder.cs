using Console_DataConnector.DataModule.DataADO.ADODbModelService;
using Console_DataConnector.DataModule.DataADO.ADOWebApiService;
using Console_DataConnector.DataModule.DataCommon.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace Console_DataConnector.DataModule.DataADO.ADODbMigBuilderService
{
    public class SqlServerMigBuilder : SqlServerDbModel, IdbMigBuilder
    {
        protected IdDLFactory DDLFactory = new SqlServerDDLFactory();

        public SqlServerMigBuilder(): base()
        {
        }

        public SqlServerMigBuilder(string server, string database) : base(server, database)
        {
        }

        public SqlServerMigBuilder(string server, string database, bool trustedConnection, string userId, string password) : base(server, database, trustedConnection, userId, password)
        {
        }

        private string CreateTable(System.Type EntityType) 
            => DDLFactory.CreateTable(EntityType);


        public void CreateDatabase()
        {
            foreach (var sql in DropAndCreate())
            {
                this.Info(sql.Up);
                try
                {
                    PrepareQuery(sql.Down);
                }
                catch (Exception ex)
                {
                    this.Error($"Ошибка при выполнении {sql.Down}: {ex.Message}");
                }
                try
                {
                    PrepareQuery(sql.Up);
                }
                catch (Exception ex)
                {
                    this.Error($"Ошибка при выполнении {sql.Up}: {ex.Message}");

                }
            }
        }



        /// <summary>
        /// Команды создания структуры данных
        /// </summary>       
        public DbMigCommand[] DropAndCreate()
        {
            this.Info($"DropAndCreate()");
            List<DbMigCommand> commands = new List<DbMigCommand>();
            InputConsole.Clear();
            //таблицы
            foreach (Type EntityType in EntityTypes)
            {
                var TableMetaData = DDLFactory.CreateTableMetaData(EntityType);
                string TableSchema = TableMetaData.schema;
                string TableName = TableMetaData.name;
                string DropTable = @"DROP TABLE " + TableName;                                
                var table = CreateTable(EntityType);
                this.Info(table);

                commands.Add(new DbMigCommand($"{"\n"}" +
                    table,
                    DropTable, commands.Count()));

                //PrepareQuery(DropTable);
                //PrepareQuery(table);
            }


            //внешние ключи
            foreach (Type EntityType in EntityTypes)
            {
                foreach (var ForeignKey in GetForeignKeys(EntityType))
                {
                    commands.Add(new DbMigCommand(
                        $@"ALTER TABLE {ForeignKey.SourceTable}
                                    ADD CONSTRAINT FK_{ForeignKey.SourceTable.Split(".")[1].ReplaceAll("[", "").ReplaceAll("]", "")}_{ForeignKey.SourceColumn} 
                                    FOREIGN KEY ({ForeignKey.SourceColumn}) REFERENCES {ForeignKey.TargetTable.Split(".")[1]}({ForeignKey.TargetColumn});",
                        $@"ALTER TABLE {ForeignKey.SourceTable}
                                    DROP CONSTRAINT FK_{ForeignKey.SourceTable.Split(".")[1]}_{ForeignKey.SourceColumn} ", commands.Count()));
                    //commands.Add($"ALTER TABLE {ForeignKey.SourceTable} "+
                    //    $@"ADD CONSTRAINT FK_{ForeignKey.SourceTable.ReplaceAll(".","_")}_{ForeignKey.TargetTable.ReplaceAll(".","_")} FOREIGN KEY ({ForeignKey.SourceColumn}) REFERENCE {ForeignKey.TargetTable}({ForeignKey.TargetColumn})");
                }
            }
            /*}
            catch (Exception ex)
            {
                var TypeName = MethodBase.GetCurrentMethod().DeclaringType.GetTypeName();
                var MethodName = MethodBase.GetCurrentMethod().Name;                                                
                this.Error($"Метод действия {TypeName}.{MethodName} не выполнен", ex);
            }*/

            return commands.ToArray();
        }



        private IEnumerable<KeyMetadata> GetForeignKeys(Type entityType)
        {
            var TableMetaData = DDLFactory.CreateTableMetaData(entityType);
            string TableSchema = TableMetaData.schema;
            string TableName = TableMetaData.name;

            Func<PropertyInfo, string> GetRefenceTableName = (info) =>
            {
                var RefTableMetaData = DDLFactory.CreateTableMetaData(info.PropertyType);
                string RefTableSchema = TableMetaData.schema;
                string RefTableName = TableMetaData.name;
                return $"[{RefTableSchema}].[{RefTableName}]";
            };
            return entityType.GetProperties().Where(property => IsMapped(entityType, property) && IsPrimitive(property.PropertyType) == false)
                .Select(property => new KeyMetadata()
                {
                    SourceTable = $"[{TableSchema}]" + "." + $"[{TableName}]",
                    SourceColumn = property.Name/*.ToTSQLStyle()*/ + "_Id",
                    TargetTable = GetRefenceTableName(property),
                    TargetColumn = "Id"
                }).ToArray();
        }



        private bool IsMapped(Type type, PropertyInfo property)
        {
            return 
                Utils.ForProperty(type, property.Name).ContainsKey(nameof(NotMappedAttribute)) == false &&
                Utils.ForProperty(type, property.Name).ContainsKey("NotMapped") == false;

        }

        string[] PrimitiveTypeNames = new string[]
        {
            nameof(Byte),
            nameof(IEnumerable<Byte>),
            nameof(Boolean),
            nameof(String),
            nameof(Int32),
            nameof(Int64),
            nameof(DateTime)
        };
        private bool IsPrimitive(Type propertyType)
        {
            return PrimitiveTypeNames.Contains(propertyType.Name);
        }




        public void TestProcedures()
        {
            var api = new SqlServerWebApi();
            foreach (var p in api.GetProceduresMetadata())
            {
                var input = new Dictionary<string, string>(p.Value.ParametersMetadata.Keys.Select(key => new KeyValuePair<string, string>(key, "1")));
                api.Info(input.ToJsonOnScreen());
                api.Info(api.Request($"/{p.Key}", input).Result);
            }
        }
        public void UpdateDatabase()
        {
            TryPrepareQuery(CreateTable(typeof(DbMigCommand)));

            var messages = new List<string>();
            var migration = DropAndCreate();

            foreach (var mig in migration)
            {
                //log
                string message = mig.Up
                        .ReplaceAll("  ", " ")
                        .ReplaceAll("  ", " ");
                Action todo = () =>
                {




                    //exec
                    Catch(() => PrepareQuery(message), (ex) => { this.Error(ex); });
                    this.Info(message.Split("/n"));
                };




                messages.AddRange(new List<string>(message.Split("/n")));
            }

        }

        private void Catch(Func<int> p1, Action<object> p2)
        {
            throw new NotImplementedException();
        }
    }
}
