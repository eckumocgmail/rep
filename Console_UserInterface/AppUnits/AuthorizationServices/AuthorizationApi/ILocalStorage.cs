public interface ILocalStorage
{
    Task<string> GetItem(string kEY);
    Task SetItem(string kEY, string token);
}