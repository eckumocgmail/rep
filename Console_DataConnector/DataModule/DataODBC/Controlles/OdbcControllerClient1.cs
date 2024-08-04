using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;
public class OdbcControllerHttpClientTest: TestingElement
{
    public override void OnTest()
    {
        var odbc = new OdbcControllerHttpClient("https://localhost:5001/", "UserAccounts" );
        odbc.List().ToJsonOnScreen().WriteToConsole();
    }
}


public class OdbcControllerHttpClient
{
    private readonly string baseUrl;
    private readonly string entity;
    private readonly HttpClient http;

    public OdbcControllerHttpClient(string baseUrl, string entity)
    {
        this.baseUrl = baseUrl;
        this.entity = entity;
        this.http = new HttpClient();
    }

    public async Task<List<object>> List()
    {
        var response = await this.http.GetAsync($"{baseUrl}/{entity}/list");
        response.EnsureSuccessStatusCode();
        string json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<object>>(json);
    }

    public async Task<Dictionary<string, long>> Keywords(string query) 
    {
        var response = await this.http.GetAsync($"{baseUrl}/{entity}/Keywords/{query}");
        response.EnsureSuccessStatusCode();
        string json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Dictionary<string, long>>(json);
    }

    public async Task<List<object>> Search(string query, int page, int size) 
    {
        var response = await this.http.GetAsync($"{baseUrl}/{entity}/Search/?size={size}&page={page}");
        response.EnsureSuccessStatusCode();
        string json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<object>>(json);
    }

    public async Task<List<object>> Page(string query, int page, int size) 
    {
        var response = await this.http.GetAsync($"{baseUrl}/{entity}/Page/?query={query}&page={page}&size={size}");
        response.EnsureSuccessStatusCode();
        string json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<object>>(json);
    }
    public async Task<int> Delete(int id) 
    {
        var response = await this.http.GetAsync($"{baseUrl}/{entity}/Delete/?id={id}");
        response.EnsureSuccessStatusCode();
        string json = await response.Content.ReadAsStringAsync();
        return int.Parse(json);
    }

    public async Task<object> Find(int id)
    {
        var response = await this.http.GetAsync($"{baseUrl}/{entity}/Find/?id={id}");
        response.EnsureSuccessStatusCode();
        string json = await response.Content.ReadAsStringAsync();
        return int.Parse(json);
    }
    public async Task<int> Update(int id, Dictionary<string, object> data) 
    {
        var response = await this.http.PutAsync($"{baseUrl}/{entity}/Update/?id={id}", new StringContent(JsonConvert.SerializeObject(data)));
        response.EnsureSuccessStatusCode();
        string json = await response.Content.ReadAsStringAsync();
        return int.Parse(json);
    }
    public async Task<int> Create(Dictionary<string, object> data) 
    {        
        var response = await this.http.PostAsync($"{baseUrl}/{entity}/Create/", new StringContent(JsonConvert.SerializeObject(data)));
        response.EnsureSuccessStatusCode();
        string json = await response.Content.ReadAsStringAsync();
        return int.Parse(json);
    }

}
   