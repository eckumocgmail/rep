using System.Collections.Generic;

namespace Console_BlazorApp.AppUnits.DeliveryApi
{
    public interface IEfEntityFasade<TEntity> where TEntity : class
    {
        int Create(params TEntity[] items);
        IEnumerable<TEntity> Get(params int[] ids);
        IEnumerable<TEntity> GetAll();
        IEnumerable<string> GetOptions(string searchQuery);
        IEnumerable<TEntity> GetPage(int page, int size);
        IEnumerable<TEntity> GetPage(int[] ids, int page, int size);
        int Remove(params int[] ids);
        IEnumerable<TEntity> Search(string query);
        int TotalPages(string searchQuery, int v);
        IEnumerable<object> SearchColumnsInPropertiesPage(string query, string[] properties, string[] columns, int page, int size);
        int TotalResults(string searchQuery);
        IEnumerable<object> SearchInProperties(string query, string[] properties);
        IEnumerable<object> SearchInPropertiesPage(string query, string[] properties, int page, int size);
        IEnumerable<TEntity> SearchPage(string query, int page, int size);
        int Update(params TEntity[] items);
    }
}
