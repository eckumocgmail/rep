using Console_DataConnector.DataModule.DataModels.MessageModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataModels.MessageService
{
    public interface IMessageService 
    {
        public int UpdateMessageProperty(int MessageProtocolId, MessageProperty property);
        public int DeleteMessageProperty(int MessageProtocolId, MessageProperty property);        
        public int AddMessageProperty(int MessageProtocolId, MessageProperty property, List<MessageAttribute> attrs );
        public IEnumerable<MessageProperty> GetMessageProperties(int MessageProtocolId );
        public IEnumerable<MessageProtocol> GetMessageProtocols( );
        public int AddMessageProtocol( MessageProtocol target );

    }

    public class MessageService: IMessageService
    {
        private readonly MessageWebApi api;

        public MessageService(MessageWebApi api)
        {

            this.api = api;
        }

        public int UpdateMessageProperty(int MessageProtocolId, MessageProperty property)
        {
            throw new NotImplementedException();
        }

        public int DeleteMessageProperty(int MessageProtocolId, MessageProperty property)
        {
            throw new NotImplementedException();
        }

        public int AddMessageProperty(int MessageProtocolId, MessageProperty property, List<MessageAttribute> attrs)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MessageProperty> GetMessageProperties(int MessageProtocolId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MessageProtocol> GetMessageProtocols()
        {
            throw new NotImplementedException();
        }

        public int AddMessageProtocol(MessageProtocol target)
        {
            throw new NotImplementedException();
        }
    }
}
