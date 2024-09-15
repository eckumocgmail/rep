using Parlot;

namespace pickpoint_delivery_service
{
    internal class WebApiDetaliusBotTestsClient
    {
        private string v;
        private HttpClient http;

        public WebApiDetaliusBotTestsClient(string v, HttpClient http)
        {
            this.v = v;
            this.http = http;
        }

        public async Task<TokenResult> GetTokenAsync(object value)
        {
            await Task.CompletedTask;
            return new TokenResult() 
            { 
                
            };
        }

        internal void SetToken(object access_token)
        {
            throw new NotImplementedException();
        }
    }
}