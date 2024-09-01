using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataModels.MessageService
{
    public class MessageWebApiTest : TestingElement<MessageWebApi>
    {
        public override void OnTest()
        {
            var api = new MessageWebApi();
            api.DropAndCreate();

            api.InitMessageAttributes();
            var protocolFasade = api.GetMessageProtocol();
            this.Info(protocolFasade.GetAll().ToJsonOnScreen());
            int protocolId = 0;
            this.Info(protocolId = protocolFasade.Create(new MessageProtocol()
            {
                Name = "Logs"
            }));

            var properetyFasade = api.GetMessageProperty();
            InputConsole.Clear();
            int id = properetyFasade.Create(new MessageProperty()
            {
                MessageProtocolId = protocolId,
            });
            this.Info(properetyFasade.GetAll().ToJsonOnScreen());

            id = properetyFasade.Create(new MessageProperty()
            {
                MessageProtocolId = protocolId,
            });
            this.Info(properetyFasade.GetAll().ToJsonOnScreen());
            properetyFasade.Remove(id);
            this.Info(properetyFasade.GetAll().ToJsonOnScreen());
            properetyFasade.Update(new MessageProperty()
            {
                Id = properetyFasade.GetAll().First().Id,
                Label = "ТЕст"
            });
            this.Info(properetyFasade.GetAll().ToJsonOnScreen());
        }
    }
}
