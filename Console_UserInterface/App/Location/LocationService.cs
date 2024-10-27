using Microsoft.EntityFrameworkCore;

using static Console_UserInterface.Services.Location.LocationDbContext;


namespace Console_UserInterface.Services.Location
{

    /// <summary>
    /// Сервис виртуальной навигации
    /// </summary>
    public class LocationService
    {
        private readonly LocationDbContext locationDbContext;

        public LocationService(LocationDbContext locationDbContext)
        {
            this.locationDbContext = locationDbContext;
        }

        /// <summary>
        /// Переход на другой адрес
        /// </summary>
        /// <param name="uri">относительный путь</param>
        /// <returns>страница компонентов</returns>
        public AppPage Navigate(string uri)
        {
            var route = locationDbContext.AppRoutes.FirstOrDefault(route => route.Uri == uri);
            if (route == null)
            {
                return null;
            }
            else
            {
                return locationDbContext.AppPages.Include(p => p.PageComponents).FirstOrDefault(p => p.Id == route.AppPageId);
            }
        }
    }
}
