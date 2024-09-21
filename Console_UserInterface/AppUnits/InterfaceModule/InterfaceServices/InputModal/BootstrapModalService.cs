using Microsoft.JSInterop;

namespace Console_UserInterface.AppUnits.InterfaceModule.InterfaceServices.InputModal
{
    public interface IBootstrapModalService
    {
        public Task<TComponent> OpenModalDialog<TComponent>(TComponent instance);
    }
    public class BootstrapModalService : IBootstrapModalService
    {
        private readonly IJSRuntime js;

        public BootstrapModalService(IJSRuntime js)
        {
            this.js = js;
        }

        public async Task<TComponent> OpenModalDialog<TComponent>(TComponent instance)
        {
            await js.InvokeAsync<int>("openModalDialog", $"modal_{instance.GetHashCode()}");
            return instance;
        }
    }
}
