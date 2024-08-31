using Console_DataConnector.DataModule.DataModels.MessageModel;

using System.Linq;

namespace Console_ApiTests
{
    public class ConsoleDataConnectorUnit
    {
        public void Test()
        {
            var api = new MessageWebApi();
            api.Info(api.Services.Select(service => service.GetEntityName()).ToJsonOnScreen());

        }
    }
}