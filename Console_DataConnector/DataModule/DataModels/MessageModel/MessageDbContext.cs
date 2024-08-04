using Console_DataConnector.DataModule.DataADO.ADOWebApiService;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataModels.MessageModel
{
    public class MessageDbContext : SqlServerWebApi
    {
        public MessageDbContext()
        {
            AddEntityType(typeof(MessageAttribute));
            AddEntityType(typeof(MessageProperty));
            AddEntityType(typeof(MessageProtocol));
            AddEntityType(typeof(ValidationModel));
        }
    }
}
