namespace Console_BlazorApp.AppUnits.DeliveryApi
{
    public interface IEfEntitySearch<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetSearchResults(string query);
        IEnumerable<TEntity> GetSearchResultsByPage(string query, int page, int size);
        IEnumerable<object> SearchInProperties(string query, string[] properties);
        IEnumerable<object> SearchInPropertiesByPage(string query, string[] properties, int page, int size);
        IEnumerable<object> SearchInPropertiesAndSelecColumns(string query, string[] properties, string[] columns, int page, int size);
    }
}
