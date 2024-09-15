namespace pickpoint_delivery_service
{
    internal class WebApiDetaliusBotTestsSpareClient
    {
        private string v;
        private HttpClient http;

        public WebApiDetaliusBotTestsSpareClient(string v, HttpClient http)
        {
            this.v = v;
            this.http = http;
        }

        internal async Task<List<SparePart>> PartAsync(object product_id)
        {
            throw new NotImplementedException();
        }
    }
}