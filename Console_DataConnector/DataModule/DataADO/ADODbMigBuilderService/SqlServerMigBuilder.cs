


using Console_DataConnector.DataModule.DataADO.ADODbModelService;
using Console_DataConnector.DataModule.DataCommon.Metadata;

using DataModule;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataADO.ADODbMigBuilderService
{
    public class SqlServerMigBuilder : SqlServerDbModel, IDbMigBuilder
    {





        protected IDDLFactory DDLFactory = new SqlServerDDLFactory();



        public SqlServerMigBuilder()
        {
        }

        public SqlServerMigBuilder(string server, string database) : base(server, database)
        {
        }

        public SqlServerMigBuilder(string server, string database, bool trustedConnection, string userID, string password) : base(server, database, trustedConnection, userID, password)
        {
        }



        private string CreateTable(Type EntityType) 
            => DDLFactory.CreateTable(EntityType);


        public void CreateDatabase()
        {
            foreach (var sql in DropAndCreate())            
                PrepareQuery(sql.Up);            
        }



        /// <summary>
        /// Команды создания структуры данных
        /// </summary>       
        public DbMigCommand[] DropAndCreate()
        {
            Console.WriteLine($"DropAndCreate()");
            List<DbMigCommand> commands = new List<DbMigCommand>();
            try
            {
                //таблицы
                foreach (Type EntityType in EntityTypes)
                {
                    var TableMetaData = DDLFactory.CreateTableMetaData(EntityType);
                    string TableSchema = TableMetaData.schema;
                    string TableName = TableMetaData.name;
                    string DropTable = @"DROP TABLE " + TableName;

                    commands.Add(new DbMigCommand($"{"\n"}" +
                        CreateTable(EntityType),
                        DropTable, commands.Count()));
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
            }
            catch (Exception ex)
            {
                var TypeName = MethodBase.GetCurrentMethod().DeclaringType.GetTypeName();
                var MethodName = MethodBase.GetCurrentMethod().Name;                                                
                this.Error($"Метод действия {TypeName}.{MethodName} не выполнен", ex);
            }

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
                    SourceColumn = property.Name.ToTSQLStyle() + "_ID",
                    TargetTable = GetRefenceTableName(property),
                    TargetColumn = "ID"
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
                    Console.WriteLine(message.Split("/n"));
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
