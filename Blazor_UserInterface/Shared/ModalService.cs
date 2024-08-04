using Microsoft.JSInterop;

namespace Console_BlazorApp.Shared
{
    public class ModalService : IModalService
    {
        private readonly IJSRuntime _js;

        public ModalService(IJSRuntime js)
        {
            this._js = js;
        }

        public IModalReference<T> Show<T>(string title, ModalParameters parameters, ModalOptions options)
        {

            _js.InvokeAsync<string>($"");



            return null;
        }
    }
}