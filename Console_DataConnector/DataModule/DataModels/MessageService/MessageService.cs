using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataModels.MessageService
{
    public interface IMessageService 
    {
        public int UpdateMessageProperty(MessageProperty property);
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

        public int UpdateMessageProperty(MessageProperty property)
        {            
            return this.api.GetMessageProperty().Update(property);
        }

        public int DeleteMessageProperty(int MessageProtocolId, MessageProperty property)
        {
            property.Id = MessageProtocolId;             
            return this.api.GetMessageProperty().Create(property);
        }

        public int AddMessageProperty(int MessageProtocolId, MessageProperty property, List<MessageAttribute> attrs)
        {
            property.Id = MessageProtocolId;
            foreach(var attr in attrs) 
            {                
                property.Attribute = attr;
            }
            return this.api.GetMessageProperty().Create(property);
        }

        public IEnumerable<MessageProperty> GetMessageProperties(int MessageProtocolId)
        {
            return this.api.GetMessageProperty().GetAll().Where(property => property.MessageProtocolId == MessageProtocolId);
        }

        public IEnumerable<MessageProtocol> GetMessageProtocols()
        {
            return api.GetMessageProtocol().GetAll();
        }

        public int AddMessageProtocol(MessageProtocol target)
        {
            return api.GetMessageProtocol().Create(target);
        }

        public int RemoveMessageProtocol(int id)
        {
            return api.GetMessageProtocol().Remove(id);
        }

        public int UpdateMessageProtocol(MessageProtocol target)
        {
            return api.GetMessageProtocol().Update(target);
        }
    }
}
