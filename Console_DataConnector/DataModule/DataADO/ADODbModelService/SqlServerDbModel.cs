﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Console_DataConnector.DataModule.DataADO.ADODbMetadataServices;

namespace Console_DataConnector.DataModule.DataADO.ADODbModelService
{
    public class SqlServerDbModel : SqlServerDbMetadata, IdbModel
    {
        public ISet<Type> EntityTypes { get; set; }

        public SqlServerDbModel() : base()
        {
            EntityTypes = new HashSet<Type>();
        }

        public SqlServerDbModel(string server, string database) : base(server, database)
        {
            EntityTypes = new HashSet<Type>();
        }

        public SqlServerDbModel(string server, string database, bool trustedConnection, string userId, string password) : base(server, database, trustedConnection, userId, password)
        {
            EntityTypes = new HashSet<Type>();

        }

        public Type[] GetEntityClasses() => EntityTypes.ToArray();
        public void AddEntityType(Type entity) => EntityTypes.Add(entity);

    }
}
