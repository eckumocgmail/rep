using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataADO.ADODbModelService
{
    public interface IdbModel
    {
        public ISet<Type> EntityTypes { get; set; }
        public Type[] GetEntityClasses();
        public void AddEntityType(Type entity);
    }
}
