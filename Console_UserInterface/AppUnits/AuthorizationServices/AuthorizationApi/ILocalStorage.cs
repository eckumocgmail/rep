public interface ILocalStorage
{
    Task SetToken(string token);
    Task<string> GetToken();
    Task<string> GetItem(string kEY);
    Task SetItem(string kEY, string token);
}