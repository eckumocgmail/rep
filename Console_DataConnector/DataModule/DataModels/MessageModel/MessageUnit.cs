using Console_BlazorApp.AppUnits.DeliveryApi;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataModels.MessageModel
{
    public class MessageUnit: IDisposable
    {
        private readonly MessageDbContext _messageDbContext;

        private readonly IEfEntityFasade<MessageAttribute> MessageAttributeFasade;
        private readonly IEfEntityFasade<MessageProtocol> MessageProtocolFasade;
        private readonly IEfEntityFasade<MessageProperty> MessagePropertyFasade;
        private readonly IEfEntityFasade<ValidationModel> ValidationModelFasade;

        public MessageUnit(MessageDbContext messageDbContext)
        {
            this._messageDbContext = messageDbContext;
            this.MessageAttributeFasade = new EfEntityFasade<MessageAttribute>(_messageDbContext);
            this.MessagePropertyFasade = new EfEntityFasade<MessageProperty>(_messageDbContext);
            this.ValidationModelFasade = new EfEntityFasade<ValidationModel>(_messageDbContext);
            this.MessageProtocolFasade = new EfEntityFasade<MessageProtocol>(_messageDbContext);
        }

        public void Dispose()
        {
            this._messageDbContext.Dispose();
        }
    }
}
